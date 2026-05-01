using UnityEngine;

public class EnterShopUI : MonoBehaviour
{
    public GameObject enterShopButton;
    public GameObject shopPanel;
    public GameObject mainMenuPanel;

    public void OpenShop()
    {
        enterShopButton.SetActive(false);
        shopPanel.SetActive(true);
        mainMenuPanel.SetActive(true);
    }
}