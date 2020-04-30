using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public List<Objective> objectives;
    public Objective currentObjective;
    private Objective previousObjective;

    public TextMeshProUGUI tutorialBox;

    private PlayerManager playerManager;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        objectives[0].active = true;
        currentObjective = objectives[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObjective != previousObjective)
        {
            previousObjective = currentObjective;
            tutorialBox.SetText(currentObjective.objective);
        }
        if(currentObjective.complete)
        {
            objectives.Remove(currentObjective);
            if (objectives.Count > 0)
                currentObjective = objectives[0];
            else
                tutorialBox.SetText("");
        }

        switch(currentObjective.goal)
        {
            case Goal.move:
                if(playerMovement.movement != Vector2.zero)
                {
                    currentObjective.complete = true;
                }
                break;
            case Goal.interact:
                if(playerManager.inventory.Count > 0)
                {
                    currentObjective.complete = true;
                }
                break;
            case Goal.inventory:
                if(playerManager.inventoryMenu.activeInHierarchy)
                {
                    currentObjective.complete = true;
                }
                break;
            case Goal.crafting:
                if(!playerManager.uISelection)
                {
                    currentObjective.complete = true;
                }
                break;
            case Goal.equipItem:
                if(playerManager.leftHand != null || playerManager.rightHand != null)
                {
                    currentObjective.complete = true;
                }
                break;
        }
    }

    [System.Serializable]
    public class Objective
    {
        public Goal goal;

        public string objective;
        public bool active;
        public bool complete;

        public Objective(string objective)
        {
            this.objective = objective;
            complete = false;
            active = false;
        }
    }

    public enum Goal
    {
        move,
        interact,
        inventory,
        crafting,
        equipItem,
        NOTYETIMPLEMENTED
    }
}



