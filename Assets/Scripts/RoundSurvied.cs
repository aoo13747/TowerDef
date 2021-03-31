using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSurvied : MonoBehaviour
{
    public Text roundText;

    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(0.8f);

        while(round < PlayerStats.Rounds)
        {
            round++;
            roundText.text = round.ToString();

            yield return new WaitForSeconds(0.05f);
        }
    }
}
