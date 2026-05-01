using UnityEngine;

public class DialogueButton : MonoBehaviour
{
    public void NextDialogue()
    {
        NPCDialogueController.NextCurrentDialogue();
    }

    public void EndDialogue()
    {
        NPCDialogueController.EndCurrentDialogue();
    }
}