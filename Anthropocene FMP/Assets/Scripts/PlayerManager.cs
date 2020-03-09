using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float oxygen = 100f;
    public float rateOfOxygenDecrease = 0.4f;
    public float food = 100f;
    public float rateOfFoodDecrease = 0.5f;
    public float water = 100f;
    public float rateOfWaterDecrease = 0.2f;
    public float rateOfHealthDecrease = 2f;

    public float currentPlayerHealth = 80;
    public float maxPlayerHealth = 80;

    public List<Item> inventory = new List<Item>();
    Interactable interactable;
    GameUI GUI;
    private void Start()
    {
        GUI = FindObjectOfType<GameUI>();
        GUI.infoText.text = null;
        GUI.commandText.text = null;
    }

    void Update()
    {
        oxygen -= rateOfOxygenDecrease * Time.deltaTime;
        water -= rateOfWaterDecrease * Time.deltaTime;
        if (oxygen <= 0)
        {
            oxygen = 0;
            currentPlayerHealth -= rateOfHealthDecrease * Time.deltaTime;
            //no oxygen functionality
        }
        if (food <= 0)
        {
            food = 0;
            //no food functionality
        }
        if (water <= 0)
        {
            water = 0;
            //no water functionality
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactable != null)
            {
                interactable.interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<Interactable>();
        if(interactable != null)
        {
            GUI.commandText.text = "E";
            if (interactable.item != null)
            {
                GUI.infoText.text = interactable.item.itemName;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = null;
        GUI.commandText.text = null;
        GUI.infoText.text = null;
    }
}
