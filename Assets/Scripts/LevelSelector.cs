using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] levelButtons;
    public string mainmenuName = "MainMenu";

    private void Start()
    {
        PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0;  i < levelButtons.Length; i++)
        {
            if(i+1 > PlayerPrefs.GetInt("levelReached", 1))
            levelButtons[i].interactable = false;
        }
    }

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public void ResetGameProgress()
    {
        PlayerPrefs.DeleteKey("levelReached");
        fader.FadeTo(mainmenuName);
    }

}
