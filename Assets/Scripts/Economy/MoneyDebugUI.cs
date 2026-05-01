using UnityEngine;

public class MoneyDebugUI : MonoBehaviour
{
    public int amountToAdd = 1;
    public int amountToSubtract = 1;

    public void AddMoney()
    {
        PlayerEconomy.Instance.AddGold(amountToAdd);
        Debug.Log("Gold after adding: " + PlayerEconomy.Instance.currentGold);

        ShopUI shopUI = FindObjectOfType<ShopUI>();
        if (shopUI != null)
        {
            shopUI.RefreshGold();
        }
    }

    public void SubtractMoney()
    {
        PlayerEconomy.Instance.SpendGold(amountToSubtract);
        Debug.Log("Gold after subtracting: " + PlayerEconomy.Instance.currentGold);

        ShopUI shopUI = FindObjectOfType<ShopUI>();
        if (shopUI != null)
        {
            shopUI.RefreshGold();
        }
    }
}