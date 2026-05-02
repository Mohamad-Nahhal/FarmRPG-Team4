using UnityEngine;

public class FriendshipSystem : MonoBehaviour
{
    [SerializeField] private string _npcName;
    [SerializeField] private int _friendshipPoints = 0;
    [SerializeField] private int _maxFriendship = 100;

    public string NPCName => _npcName;
    public int FriendshipPoints => _friendshipPoints;

    public void AddFriendship(int amount)
    {
        _friendshipPoints += amount;

        if (_friendshipPoints > _maxFriendship)
            _friendshipPoints = _maxFriendship;

        if (_friendshipPoints < 0)
            _friendshipPoints = 0;

        Debug.Log(_npcName + " friendship: " + _friendshipPoints);
    }
}