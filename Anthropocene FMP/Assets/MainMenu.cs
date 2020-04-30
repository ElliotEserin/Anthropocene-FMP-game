using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button continueButton;

    private void Start()
    {
        if(PlayerPrefs.GetInt("STATUS", 0) >= 1)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void StartNewGame(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void LoadGame()
    {
        Debug.Log("Load game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
