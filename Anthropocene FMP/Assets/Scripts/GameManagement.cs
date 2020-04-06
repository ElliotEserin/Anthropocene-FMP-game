using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManagement : MonoBehaviour
{
    List<SpriteRenderer> foregroundObjects = new List<SpriteRenderer>();
    public float timeBetweenWeather = 5f; //minutes
    public Weather currentWeather;
    public ParticleSystem rain, storm, wind, blizzard;
    ParticleSystem currentParticleActive;
    PlayerManager playerManager;
    float timer = 0;
    public float timerMultiplier = 1;
    public float rainDamage = 1f;
    public float stormDamage = 2f;
    public float windDamage = 1f;
    public float blizzardDamage = 1f;

    void Start()
    {
        foreach(SpriteRenderer i in FindObjectsOfType<SpriteRenderer>())
        {
            if (i.sortingLayerName == "Foreground")
            {
                foregroundObjects.Add(i);
                i.sortingOrder = (int)(i.gameObject.transform.position.y*-100);
            }
        }
        //string[] items = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Items" });

        //foreach(string i in items)
        //{
        //    Item item = (Item)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(i), typeof(Item));
        //    item.quantity = 0;
        //}

        playerManager = FindObjectOfType<PlayerManager>();
    }

    private void Update()
    {      
        if (timer >= timeBetweenWeather * 60)
        {
            if(currentParticleActive != null)
                currentParticleActive.Stop();

            int chance = Random.Range(0, 100);
            
            if(chance <= 60)
            {
                currentWeather = Weather.clear;
                currentParticleActive = null;
            }
            else if(chance < 70)
            {
                currentWeather = Weather.raining;
                currentParticleActive = rain;
            }
            else if(chance < 75)
            {
                currentWeather = Weather.storm;
                currentParticleActive = storm;
            }
            else if(chance < 95)
            {
                currentWeather = Weather.windy;
                currentParticleActive = wind;
            }
            else
            {
                currentWeather = Weather.blizzard;
                currentParticleActive = blizzard;
            }
            if (currentParticleActive != null)
                currentParticleActive.Play();

            timer = 0;
        }
        else
        {
            timer += Time.deltaTime * timerMultiplier;
        }

        if (!playerManager.isCovered)
        {
            switch (currentWeather)
            {
                case Weather.raining:
                    playerManager.currentPlayerHealth -= rainDamage * Time.deltaTime;
                    break;
                case Weather.storm:
                    playerManager.currentPlayerHealth -= stormDamage * Time.deltaTime;
                    break;
                case Weather.windy:
                    playerManager.oxygen -= windDamage * Time.deltaTime;
                    break;
                case Weather.blizzard:
                    playerManager.food -= blizzardDamage * Time.deltaTime;
                    break;
                case Weather.clear:
                    break;
            }
        }
    }
}

public enum Weather
{
    clear,
    raining,
    storm,
    windy,
    blizzard
}
