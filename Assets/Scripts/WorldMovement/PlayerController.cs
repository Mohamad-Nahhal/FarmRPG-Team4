using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject tileHighlight;
     [SerializeField] private GameObject UI_PlayerInv;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movementInput;
    private Vector3 originalScale;

    public Vector2 lastMoveDir = Vector2.down;
    private Vector2Int currentGridPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        //PlayerPrefs.DeleteKey("spawnX");
        //PlayerPrefs.DeleteKey("spawnY");
    }
    private void Start()
    {
        

        Debug.Log("Spawned at: " + transform.position);
    }

   

    private void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleFlip();
        UpdateGridPosition();
        HandleHighlight();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
        {
            movementInput = new Vector2(horizontal, 0);
            lastMoveDir = movementInput;
        }
        else if (vertical != 0)
        {
            movementInput = new Vector2(0, vertical);
            lastMoveDir = movementInput;
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    private void Move()
    {
        rb.linearVelocity = movementInput * moveSpeed;
    }

    private void HandleAnimation()
    {
        if (movementInput != Vector2.zero)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("walk"))
                anim.Play("walk");
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                anim.Play("idle");
        }
    }

    private void HandleFlip()
    {
        if (movementInput.x != 0)
        {
            Vector3 scale = originalScale;
            scale.x = originalScale.x * Mathf.Sign(movementInput.x);
            transform.localScale = scale;
        }
    }

    private void UpdateGridPosition()
    {
        if (gridManager == null) return;

        currentGridPosition = gridManager.GetGridPosition(transform.position);
    }

    public Vector2Int GetFacingGridPosition()
    {
        Vector3 targetWorldPos = transform.position + (Vector3)lastMoveDir;
        return gridManager.GetGridPosition(targetWorldPos);
    }

    public Vector3 GetFacingWorldCenterPosition()
    {
        Vector2Int targetGrid = GetFacingGridPosition();
        Vector3 worldPos = gridManager.GetWorldPosition(targetGrid);
        worldPos.x += 0.5f;
        worldPos.y += 0.5f;
        return worldPos;
    }

    private void HandleHighlight()
    {
        if (tileHighlight == null || gridManager == null) return;

        bool isMoving = movementInput != Vector2.zero;
        tileHighlight.SetActive(!isMoving);

        if (!isMoving)
        {
            tileHighlight.transform.position = GetFacingWorldCenterPosition();
        }
    }
 

private IEnumerator TeleportRoutine(Vector2 position)
{
    yield return StartCoroutine(ScreenFader.Instance.FadeOut());

    TeleportTo(position);

    yield return new WaitForSecondsRealtime(0.1f);

    yield return StartCoroutine(ScreenFader.Instance.FadeIn());
}

private void TeleportTo(Vector2 position)
{
    rb.linearVelocity = Vector2.zero;
    transform.position = new Vector3(position.x, position.y, transform.position.z);
}
    private void TryInteract()
{
    Vector2Int targetGrid = GetFacingGridPosition();

    Debug.Log("Grid position: " + targetGrid);

    TileData tile = gridManager.GetTile(targetGrid);

    if (tile == null)
    {
        Debug.Log("NO TILE FOUND");
        return;
    }

    Debug.Log("Tile found. Type: " + tile.Type);

    if (tile.Type == TileType.Stamina)
    {
        StaminaSystem stamina = FindAnyObjectByType<StaminaSystem>();

        if (stamina != null)
        {
            stamina.RestoreToMax();
            Debug.Log("Stamina restored!");
        }
         TimeSystem timeSystem = FindAnyObjectByType<TimeSystem>();

    if (timeSystem != null)
    {
        timeSystem.SleepToNextDay();
    }

    Debug.Log("Slept. Stamina restored and day advanced.");

    return;
}
    if (tile.Type == TileType.Shop)
    {
        FarmShopUI shop = FindAnyObjectByType<FarmShopUI>();

        if (shop != null)
        {
            shop.OpenShop();
            Debug.Log("Shop opened!");
        }

        return;
    }

    if (tile.Type == TileType.Door)
    {
        Debug.Log("Door activated!");

       StartCoroutine(TeleportRoutine(tile.SpawnPosition));


        
    }
    
}
}
