using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class TurretBluePrint
{
    public GameObject prefab;
    public int Cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return Cost / 2;
    }
       
}
