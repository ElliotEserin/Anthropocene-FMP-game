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

    void Update()
    {
        oxygen -= rateOfOxygenDecrease * Time.deltaTime;
        food -= rateOfFoodDecrease * Time.deltaTime;
        water -= rateOfWaterDecrease * Time.deltaTime;
    }
}
