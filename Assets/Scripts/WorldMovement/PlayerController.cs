using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject tileHighlight;

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
        float x = PlayerPrefs.GetFloat("spawnX", transform.position.x);
        float y = PlayerPrefs.GetFloat("spawnY", transform.position.y);

        transform.position = new Vector3(x, y, 0);

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

    private void HandleHighlight()
    {
        if (tileHighlight == null || gridManager == null) return;

        bool isMoving = movementInput != Vector2.zero;
        tileHighlight.SetActive(!isMoving);

        if (!isMoving)
        {
            Vector2Int targetGrid = GetFacingGridPosition();

            Vector3 worldPos = gridManager.GetWorldPosition(targetGrid);

        
            worldPos.x += 0.5f;
            worldPos.y += 0.5f;

            tileHighlight.transform.position = worldPos;
        }
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

    if (tile.Type == TileType.Door)
    {
        Debug.Log("Door activated!");

        PlayerPrefs.SetString("lastDoorID", tile.DoorID);

        PlayerPrefs.SetFloat("spawnX", tile.SpawnPosition.x);
        PlayerPrefs.SetFloat("spawnY", tile.SpawnPosition.y);


        SceneLoader.Instance.LoadScene(tile.TargetScene);
    }
}
}