using UnityEngine;
using TMPro;

public class FriendshipMenuUI : MonoBehaviour
{
    public FriendshipSystem farmer;
    public FriendshipSystem shopkeeper;
    public FriendshipSystem villager;

    public TextMeshProUGUI farmerText;
    public TextMeshProUGUI shopkeeperText;
    public TextMeshProUGUI villagerText;

    public GameObject panel;

    private void Start()
    {

        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            if (panel != null)
            {
                panel.SetActive(!panel.activeSelf);
            }
        }

        if (farmer != null && farmerText != null)
            farmerText.text = "Farmer: " + farmer.FriendshipPoints;

        if (shopkeeper != null && shopkeeperText != null)
            shopkeeperText.text = "Shopkeeper: " + shopkeeper.FriendshipPoints;

        if (villager != null && villagerText != null)
            villagerText.text = "Villager: " + villager.FriendshipPoints;
    }
}