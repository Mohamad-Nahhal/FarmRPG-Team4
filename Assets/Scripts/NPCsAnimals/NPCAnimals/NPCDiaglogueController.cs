using UnityEngine;
using TMPro;

public class NPCDialogueController : MonoBehaviour
{
    public static NPCDialogueController CurrentNPC;

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _friendshipText;
    [SerializeField] private string[] _dialogueLines;

    private int _currentLine = 0;
    private FriendshipSystem _friendshipSystem;

    private void Start()
    {
        _friendshipSystem = GetComponent<FriendshipSystem>();

        if (_dialoguePanel != null)
            _dialoguePanel.SetActive(false);
    }

    public void StartDialogue()
{
    
    CurrentNPC = this;

    if (_dialogueLines == null || _dialogueLines.Length == 0) return;
    if (_dialoguePanel == null || _dialogueText == null) return;

    if (_friendshipSystem != null)
    {
        _friendshipSystem.AddFriendship(10);
    }

    _dialoguePanel.SetActive(true);
    _currentLine = 0;
    _dialogueText.text = _dialogueLines[_currentLine];

    if (_friendshipSystem != null && _friendshipText != null)
    {
       _friendshipText.text = "Friendship: " + _friendshipSystem.FriendshipPoints;
    }
}

    public void NextDialogue()
    {
        if (_dialogueLines == null || _dialogueLines.Length == 0) return;

        _currentLine++;

        if (_currentLine >= _dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        _dialogueText.text = _dialogueLines[_currentLine];
    }

    public void EndDialogue()
    {
        if (_dialoguePanel != null)
        {
            _dialoguePanel.SetActive(false);
        }

        _currentLine = 0;
    }

    public static void NextCurrentDialogue()
    {
        if (CurrentNPC != null)
        {
            CurrentNPC.NextDialogue();
        }
    }

    public static void EndCurrentDialogue()
    {
        if (CurrentNPC != null)
        {
            CurrentNPC.EndDialogue();
        }
    }
}