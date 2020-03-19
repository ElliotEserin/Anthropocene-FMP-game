﻿using System.Collections.Generic;
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
    public InventoryUI IUI;

    float lhTimer = 0f;
    float rhTimer = 0f;

    public bool uISelection = true;

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
        if(Input.GetMouseButtonDown(0) && lhTimer <= 0)
        {
            useHand(leftHand);
            lhTimer = leftHand.coolDown;
        }
        if (Input.GetMouseButtonDown(1) && rhTimer <= 0)
        {
            useHand(rightHand);
            rhTimer = rightHand.coolDown;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            uISelection = !uISelection;
        }

        if(lhTimer > 0)
        {
            lhTimer -= Time.deltaTime;
        }
        if (rhTimer > 0)
        {
            lhTimer -= Time.deltaTime;
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

    void useHand(Item hand)
    {
        if (hand != null)
        {
            if (hand.quantity == 0)
            {
                hand = null;
            }
        }

        if (hand != null)
        {
            switch (hand.itemType)
            {
                case ItemType.consumable:
                    hand.Consume(hand);
                    if (IUI.positionInList > inventory.Count - 1) { IUI.positionInList = 0; }
                    break;
                case ItemType.ranged:
                case ItemType.melee:
                    hand.attack();
                    Debug.Log("You Attacked!");
                    break;
                
            }
        }
    }
}
