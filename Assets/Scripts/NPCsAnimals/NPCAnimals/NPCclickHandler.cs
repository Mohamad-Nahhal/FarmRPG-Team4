using UnityEngine;

public class NPCclickHandler : MonoBehaviour
{
    private NPCDialogueController _dialogueController;

    private void Start()
    {
        _dialogueController = GetComponent<NPCDialogueController>();
    }

    private void OnMouseDown()
{
    Debug.Log("Clicked: " + gameObject.name);
    _dialogueController.StartDialogue();
}
}