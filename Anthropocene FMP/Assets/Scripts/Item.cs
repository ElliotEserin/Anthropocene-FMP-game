using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Generic variables:")]
    public string itemName = "new item";
    [TextArea(3,5)]
    public string description = "an item...";
    public float weight;
    public int quantity = 0;
    public ItemType itemType;

    [Header("If consumable, it restores:")]
    public RestoreType restoreType;
    public int ammount = 0;

    [Header("If weapon, it does:")]
    public float attackRadius = 0.5f;
    public float damage = 0;
    public GameObject damageCollider;

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
        itemToConsume.quantity -= 1;
        if(itemToConsume.quantity == 0)
        {
            pm.inventory.Remove(itemToConsume);
        }
        pm.currentPlayerWeight -= itemToConsume.weight;
    }

    public void attack()
    {
        if(itemType == ItemType.melee)
        {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 mousePos = FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);

            Direction direction = Direction.Center;
            float offset = 0.5f;

            if(playerPos.x < mousePos.x)
            {
                if(playerPos.y < mousePos.y)
                {
                    direction = Direction.TopRight;
                }
                else if(playerPos.y == mousePos.y)
                {
                    direction = Direction.Right;
                }
                else if (playerPos.y > mousePos.y)
                {
                    direction = Direction.BottomRight;
                }
            }
            else if (playerPos.x == mousePos.x)
            {
                if (playerPos.y < mousePos.y)
                {
                    direction = Direction.Top;
                }
                else if (playerPos.y == mousePos.y)
                {
                    direction = Direction.Center;
                }
                else if (playerPos.y > mousePos.y)
                {
                    direction = Direction.Bottom;
                }
            }
            else if (playerPos.x > mousePos.x)
            {
                if (playerPos.y < mousePos.y)
                {
                    direction = Direction.TopLeft;
                }
                else if (playerPos.y == mousePos.y)
                {
                    direction = Direction.Left;
                }
                else if (playerPos.y > mousePos.y)
                {
                    direction = Direction.BottomLeft;
                }
            }

            Debug.Log(direction);

            //Instantiate(damageCollider, spawnPoint, Quaternion.identity);
        }
        else if(itemType == ItemType.ranged)
        {

        }
    }
}

public enum ItemType
{
    melee, //use it to attack
    ranged,
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

public enum Direction
{
    Top,
    TopLeft,
    Left,
    BottomLeft,
    Bottom,
    BottomRight,
    Right,
    TopRight,
    Center
}
