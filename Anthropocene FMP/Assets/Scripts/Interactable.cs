using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractType interactType;
    public Item item; 
    public GameObject dialogue; //replace with dialogue class
    public Item itemNeeded;

    PlayerManager playerManager;
    bool canTrigger = false;
    public bool isConsumed = true;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void interact()
    {
        if (itemNeeded != null)
        {
            foreach (Item item in playerManager.inventory)
            {
                if (item == itemNeeded)
                {
                    canTrigger = true;
                }
            }
        }
        else { canTrigger = true; }

        switch(interactType)
        {
            case InteractType.Item:
                if(item != null && canTrigger)
                {
                    bool isInInventory = false;
                    foreach(Item itemInInventory in playerManager.inventory)
                    {
                        if(itemInInventory == item)
                        {
                            item.quantity += 1;
                            isInInventory = true;
                        }
                    }
                    if (!isInInventory)
                    {
                        playerManager.inventory.Add(item);
                        if (item.quantity <= 0)
                        {
                            item.quantity = 1;
                        }
                    }

                    playerManager.CalculateWeight();
                    playerManager.AddLog("Picked up: " + item.itemName);
                    if (isConsumed)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                break;
            case InteractType.Dialogue:
                //run dialogue
                break;
            case InteractType.Crafting:
                //crafting
                break;
            case InteractType.Button:
                //button
                break;
        }
    }
}

public enum InteractType
{
    Item,
    Dialogue,
    Crafting,
    Button,
    //different things that interactables can do
}
