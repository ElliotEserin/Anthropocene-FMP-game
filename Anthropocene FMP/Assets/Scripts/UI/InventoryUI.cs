using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Text inventoryListUI, inventoryTitleUI, inventoryDetailUI;
    public int positionInList = 0;
    PlayerManager playerManager;
    List<Item> inventory = new List<Item>();
    int inventoryLength;
    [SerializeField]
    Item itemSelected;

    private void OnEnable()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.uISelection = true;
        inventory = playerManager.inventory;
        UpdateUI();
    }

    private void Update()
    {
        inventoryListUI.color = inventoryTitleUI.color = inventoryDetailUI.color = Color.black;
        if (playerManager.uISelection)
        {
            //checking if a change has been made
            if (inventory.Count > 0 && inventoryLength != inventory.Count)
            {
                UpdateUI();
            }

            //getting scroll input
            var up = Input.GetKeyDown(KeyCode.W);
            var down = Input.GetKeyDown(KeyCode.S);
            var aPressed = Input.GetKeyDown(KeyCode.A);
            var dPressed = Input.GetKeyDown(KeyCode.D);
            var ePressed = Input.GetKeyDown(KeyCode.E);

            //adjusting UI accordingly
            if (up && inventory.Count > 1)
            {
                positionInList -= 1;

                if (positionInList < 0) positionInList = inventory.Count - 1;

                UpdateUI();
            }
            if (down && inventory.Count > 1)
            {
                positionInList += 1;
                if (positionInList > inventory.Count - 1) { positionInList = 0; }
                UpdateUI();
            }
            if (ePressed && itemSelected != null)
            {
                if (itemSelected.itemType == ItemType.consumable)
                {
                    itemSelected.Consume(itemSelected);
                    if (positionInList > inventory.Count - 1) { positionInList = 0; }
                    UpdateUI();
                }
            }
            if (aPressed && itemSelected != null)
            {
                playerManager.leftHand = itemSelected;
                UpdateUI();
            }
            if (dPressed && itemSelected != null)
            {
                playerManager.rightHand = itemSelected;
                UpdateUI();
            }
        }
        else
        {
            inventoryListUI.color = inventoryTitleUI.color = inventoryDetailUI.color = Color.grey;
        }
    }

    public void UpdateUI()
    {
        //display title and total weight
        inventoryTitleUI.text = "INVENTORY: " + playerManager.currentPlayerWeight + "/" + playerManager.maxPlayerWeight + "KG";
        inventoryListUI.text = "";

        //loop through all items and display them properly
        for (int i = 0; i < inventory.Count; i++)
        {
            if (positionInList == i) //display selected items info
            {
                StringBuilder detailOutput = new StringBuilder();
                itemSelected = inventory[i];
                inventoryListUI.text += ">> ";
                detailOutput.Append("DESCRIPTION: " + itemSelected.description + "\n\n" + "WEIGHT: " + itemSelected.weight + "KG" + "\n\n");

                switch (itemSelected.itemType)
                {
                    case ItemType.resource:
                        break;
                    case ItemType.consumable:
                        detailOutput.Append("PRESS 'E' TO USE..." + "\n\n");
                        break;
                    case ItemType.melee:
                    case ItemType.ranged:
                        detailOutput.Append("COOLDOWN: " + itemSelected.coolDown + " SECOND(S)..." + "\n\n");
                        break;
                }
                detailOutput.Append("PRESS 'A' OR 'D' TO EQUIP TO LEFT OR RIGHT MOUSE BUTTON...");
                inventoryDetailUI.text = detailOutput.ToString();
            }
            StringBuilder name = new StringBuilder();
            name.Append(inventory[i].itemName); //display item name

            Item item = inventory[i];
            Item lH = playerManager.leftHand;
            Item rH = playerManager.rightHand;

            if (item == lH && item == rH) { name.Append(" (L/R)"); }
            else if (item == lH) { name.Append(" (L)"); }
            else if (item == rH) { name.Append(" (R)"); }
            name.Append(" (x" + item.quantity + ")" + "\n");

            inventoryListUI.text += name.ToString();
        }
        inventoryLength = inventory.Count;

        if (inventoryLength == 0) //reseting UI
        {
            itemSelected = null;
            inventoryDetailUI.text = null;
        }

        Debug.Log("Updated Inventory...");
    }
}
