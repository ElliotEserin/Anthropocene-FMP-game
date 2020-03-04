using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI oxygenText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI waterText;

    PlayerManager pm;

    private void Start()
    {
        pm = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {
        oxygenText.text = "Oxygen: " + Math.Round(pm.oxygen);
        foodText.text = "Food: " + Math.Round(pm.food);
        waterText.text = "Water: " + Math.Round(pm.water);
    }
}
