using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameOvered;
    public GameObject gameOverUi;
    public GameObject completeLevelUi;
    

    private void Start()
    {
        gameOvered = false;
    }
    void Update()
    {
        ChackPlayerstat();
    }
    void ChackPlayerstat()
    {
        if (gameOvered)
            return;
        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        gameOvered = true;
        //Debug.Log("Game Over!");
        gameOverUi.SetActive(true);
        
    }

    public void WinLevel()
    {
        gameOvered = true;
        completeLevelUi.SetActive(true);
    }

}
