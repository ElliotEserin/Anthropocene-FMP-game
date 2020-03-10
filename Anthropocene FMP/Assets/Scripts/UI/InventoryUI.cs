using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text inventoryListUI, inventoryTitleUI;
    PlayerManager playerManager;

    private void Update()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        inventoryTitleUI.text = "INVENTORY: " + playerManager.currentPlayerWeight + "/" + playerManager.maxPlayerWeight;
        inventoryListUI.text = "";
        foreach (Item item in playerManager.inventory)
        {
            inventoryListUI.text += item.name + "\n";
        }
    }
}
