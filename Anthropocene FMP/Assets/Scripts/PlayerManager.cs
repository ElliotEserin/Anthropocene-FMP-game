using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float oxygen = 100f;
    float rateOfOxygenDecrease = 0.4f;
    public float food = 100f;
    float rateOfFoodDecrease = 0.3f;
    public float water = 100f;
    float rateOfWaterDecrease = 0.2f;

    public List<Item> inventory = new List<Item>();
    Interactable interactable;

    void Update()
    {
        oxygen -= rateOfOxygenDecrease * Time.deltaTime;
        food -= rateOfFoodDecrease * Time.deltaTime;
        water -= rateOfWaterDecrease * Time.deltaTime;

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
        Debug.Log("Interacted...");
        interactable = collision.GetComponent<Interactable>();
        if (interactable != null)
        {
            Debug.Log("...With interactable...");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = null;
    }
}
