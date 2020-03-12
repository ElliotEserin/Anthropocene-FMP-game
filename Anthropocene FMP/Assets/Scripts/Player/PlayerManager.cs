using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float oxygen = 100f, rateOfOxygenDecrease = 0.4f; //oxygen
    public float food = 100f, rateOfFoodDecrease = 0.5f; //food
    public float water = 100f, rateOfWaterDecrease = 0.2f; //water
    public float currentPlayerHealth = 100, maxPlayerHealth = 100, rateOfHealthDecrease = 2f; //health
    public float currentPlayerWeight = 0, maxPlayerWeight = 100; //weight
    public GameObject pauseMenu, inventoryMenu; //menus
    public List<Item> inventory = new List<Item>(); //inventorys
    public Item leftHand, rightHand;

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
        if(Input.GetKeyDown(KeyCode.Escape) && !inventoryMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            SetTimeScaleForMenus();
        }
        if(Input.GetKeyDown(KeyCode.I) && !pauseMenu.activeInHierarchy)
        {
            inventoryMenu.SetActive(!inventoryMenu.activeInHierarchy);
            SetTimeScaleForMenus();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactable = collision.GetComponent<Interactable>();
        if(interactable != null)
        {
            string command = "";
            if (interactable.itemNeeded != null) { command = " (" + interactable.itemNeeded.itemName + ")"; }
            GUI.commandText.text = "E" + command;
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

    void SetTimeScaleForMenus()
    {
        if (Time.timeScale == 1) { Time.timeScale = 0f; }
        else { Time.timeScale = 1f; }
    }
}
