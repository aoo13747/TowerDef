using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public string menuSceneName = "Mainmenu";
        
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        Time.timeScale = 1;
    }
}
