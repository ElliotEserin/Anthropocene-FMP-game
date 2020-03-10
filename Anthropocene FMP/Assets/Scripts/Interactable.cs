using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractType interactType;
    public Item item; 
    public GameObject dialogue; //replace with dialogue class
    public Item itemNeeded;

    PlayerManager pm;
    bool canTrigger = false;

    private void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }
    public void interact()
    {
        if (itemNeeded != null)
        {
            foreach (Item item in pm.inventory)
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
                    pm.inventory.Add(item);
                    pm.currentPlayerWeight += item.weight;
                    Destroy(this.gameObject);
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
