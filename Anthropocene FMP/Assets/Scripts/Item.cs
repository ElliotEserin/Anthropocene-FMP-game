using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "new item", description = "an item...";
    public itemType itemType;
    public itemAttributes[] itemAttributes;
    public float weight;
}

public enum itemType
{
    weapon, //use it to attack
    resource, //use it to make other things 
    consumable, //consume it for perks
    utility //place it and interact with it
}

public enum itemAttributes
{
    healthRestoritive,
    oxygenRestoritive,
    foodRestoritive,
    waterRestoritive
}
