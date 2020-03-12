using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Generic variables:")]
    public string itemName = "new item";
    [TextArea(3,5)]
    public string description = "an item...";
    public float weight;
    public ItemType itemType;

    [Header("If consumable, it restores:")]
    public RestoreType restoreType;
    public int ammount = 0;

    public void Consume(Item itemToConsume)
    {
        PlayerManager pm = FindObjectOfType<PlayerManager>();
        switch(restoreType)
        {
            case RestoreType.Health:
                pm.currentPlayerHealth += ammount;
                if (pm.currentPlayerHealth > pm.maxPlayerHealth) { pm.currentPlayerHealth = pm.maxPlayerHealth; }
                break;
            case RestoreType.Oxygen:
                pm.oxygen += ammount;
                if (pm.oxygen > 100) { pm.oxygen = 100; }
                break;
            case RestoreType.Food:
                pm.food += ammount;
                if (pm.food > 100) { pm.food = 100; }
                break;
            case RestoreType.Water:
                pm.water += ammount;
                if (pm.water > 100) { pm.water = 100; }
                break;
        }
        pm.inventory.Remove(itemToConsume);
    }
}

public enum ItemType
{
    weapon, //use it to attack
    resource, //use it to make other things 
    consumable, //consume it for perks
    utility //place it and interact with it
}
public enum RestoreType
{
    Health,
    Oxygen,
    Food,
    Water
}
