using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public Text recipeListUI, recipeTitleUI, recipeDetailUI;
    public CraftingRecipe[] recipes;

    int positionInList = 0;

    PlayerManager pm;
    CraftingRecipe recipeSelected;

    private void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }
    private void OnEnable()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (!pm.uISelection)
        {
            //getting scroll input
            var up = Input.GetKeyDown(KeyCode.W);
            var down = Input.GetKeyDown(KeyCode.S);
            var eDown = Input.GetKeyDown(KeyCode.E);

            //adjusting UI accordingly
            if (up && recipes.Length > 1)
            {
                positionInList -= 1;
                if (positionInList < 0) { positionInList = recipes.Length - 1; }
                UpdateUI();
            }
            if (down && recipes.Length > 1)
            {
                positionInList += 1;
                if (positionInList > recipes.Length - 1) { positionInList = 0; }
                UpdateUI();
            }
            if (eDown)
            {
                bool isCraftable = true;

                foreach(Item requiredItem in recipeSelected.requiredItems)
                {
                    Item item = pm.inventory.Find(x => x.name == requiredItem.itemName);
                    if(item == null)
                    {
                        isCraftable = false;
                    }
                }
                if (isCraftable)
                {
                    recipeSelected.Craft(pm);
                    FindObjectOfType<InventoryUI>().UpdateUI();
                }
            }
        }
    }

    void UpdateUI()
    {
        recipeListUI.text = "";

        for (int i = 0; i < recipes.Length; i++)
        {
            if (positionInList == i) //display selected items info
            {
                StringBuilder detailOutput = new StringBuilder();
                recipeSelected = recipes[i];
                recipeListUI.text += ">> ";

                detailOutput.Append("RESULT: " + recipes[i].craftedItem.itemName + "\n\n");
                detailOutput.Append("INGREDIENTS: \n");

                foreach(Item item in recipeSelected.requiredItems)
                {
                    detailOutput.Append(item.itemName + "\n");
                }

                detailOutput.Append("\nPRESS 'E' TO CRAFT...");

                recipeDetailUI.text = detailOutput.ToString();
            }
            StringBuilder name = new StringBuilder();
            name.Append(recipes[i].craftedItem.itemName + "\n"); //display item name

            recipeListUI.text += name.ToString();
        }

        Debug.Log("Updated crafting system...");
    }
}

[System.Serializable]
public struct CraftingRecipe
{
    public Item[] requiredItems;
    public Item craftedItem;

    public void Craft(PlayerManager pm)
    {
        foreach(Item inventoryItem in pm.inventory)
        {
           foreach(Item requiredItem in requiredItems)
            {
                if(requiredItem == inventoryItem)
                {
                    inventoryItem.quantity -= 1;
                }
            }
        }
        if (craftedItem.quantity <= 0)
        {
            pm.inventory.Add(craftedItem);
            craftedItem.quantity = 1;
        }
        else if (craftedItem.quantity > 0)
        {
            craftedItem.quantity += 1;
        }
        pm.currentPlayerWeight += craftedItem.weight;
    }
}
