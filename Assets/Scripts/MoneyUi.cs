using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUi : MonoBehaviour
{
    public Text MoneyText;
    private void Update()
    {
        MoneyText.text = "฿" + PlayerStats.Money.ToString();
    }
}
