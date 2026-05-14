using UnityEngine;

public class NPCclickHandler : MonoBehaviour
{
    private NPCDialogueController _dialogueController;
    [SerializeField] private GameObject friendshipPanel;

    private void Start()
    {
        _dialogueController = GetComponent<NPCDialogueController>();
    }

    private void OnMouseDown()
{
    Debug.Log("Clicked: " + gameObject.name);
    _dialogueController.StartDialogue();
    if (friendshipPanel != null)
        friendshipPanel.SetActive(true);
}
}