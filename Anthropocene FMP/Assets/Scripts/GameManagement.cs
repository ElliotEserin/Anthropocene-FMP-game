using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    #region static inst
    public static GameManagement instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Multiple instances!");
            return;
        }

        instance = this;
    }
    #endregion

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

    public static bool hasDied;

    [SerializeField]
    GameObject[] objectsToHideOnDeath;
    [SerializeField]
    GameObject deathUI;

    public AudioClip[] soundtrack;
    private AudioSource audio;

    void Start()
    {
        hasDied = false;

        foreach(SpriteRenderer i in FindObjectsOfType<SpriteRenderer>())
        {
            if (i.sortingLayerName == "Foreground")
            {
                foregroundObjects.Add(i);
                i.sortingOrder = (int)(i.gameObject.transform.position.y*-100);
            }
        }

        playerManager = FindObjectOfType<PlayerManager>();

        //music setup
        audio = GetComponent<AudioSource>();
        if (!audio.playOnAwake)
        {
            audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audio.Play();
        }

        if(PlayerPrefs.GetInt("STATUS", 0) == 0)
        {
            StartCoroutine(SetStatus(1));
        }
    }

    IEnumerator SetStatus(int value)
    {
        yield return new WaitForSeconds(10);
        PlayerPrefs.SetInt("STATUS", value);
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

        if (!audio.isPlaying)
        {
            audio.clip = soundtrack[Random.Range(0, soundtrack.Length)];
            audio.Play();
        }
    }

    public void Die()
    {
        if(!hasDied)
        {
            hasDied = true;
            Time.timeScale = 0f;

            foreach(GameObject objectToHide in objectsToHideOnDeath)
            {
                objectToHide.SetActive(false);
            }

            deathUI.SetActive(true);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
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
