using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractType interactType;
    public Item item; //replace with actual item class later
    public GameObject dialogue; //same here

    PlayerManager pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }
    public void interact()
    {
        switch(interactType)
        {
            case InteractType.Item:
                if(item != null)
                {
                    pm.inventory.Add(item);
                }
                break;
            case InteractType.Dialogue:
                //run dialogue
                break;
        }
        Destroy(this.gameObject);
    }
}

public enum InteractType
{
    Item,
    Dialogue,
    //different things that interactables can do
}
