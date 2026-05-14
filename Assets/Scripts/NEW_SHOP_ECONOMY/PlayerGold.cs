using System;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    [SerializeField] private int _gold = 100;

    public int Gold => _gold;

    public event Action GoldChanged;

    public bool CanAfford(int amount)
    {
        return _gold >= amount;
    }

    public bool SpendGold(int amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        if (!CanAfford(amount))
        {
            return false;
        }

        _gold -= amount;
        GoldChanged?.Invoke();
        return true;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        _gold += amount;
        GoldChanged?.Invoke();
    }
    public void SetGold(int amount)
{
    _gold = Mathf.Max(0, amount);
    GoldChanged?.Invoke();
}
}




