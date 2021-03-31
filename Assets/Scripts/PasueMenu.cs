using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasueMenu : MonoBehaviour
{
    public GameObject ui;
    public SceneFader sceneFader;
    public string menuSceneName = "Mainmenu";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        if(ui.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Continue()
    {
        Toggle();
    }
    public void Restart()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);

    }

    public void Menu()
    {        
        sceneFader.FadeTo(menuSceneName);
        Toggle();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
