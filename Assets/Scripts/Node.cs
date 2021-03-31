using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color NotenoughMoneyColor;
    private Color startColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    public Vector3 positionOffSet;

    BuildManager buildManager;
    

    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }
   
    //1 MouseEnter
    private void OnMouseEnter()
    {        
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = NotenoughMoneyColor;
        }

    }
    //2 MouseExit
    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
    //3 MouseDown
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
         
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        //Build a Turret
        BuildTurret(buildManager.GetTurretToBuild());
        
    }
    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.Cost)
        {
            //Debug.Log("Not Enough Money");
            return;
        }

        PlayerStats.Money -= bluePrint.Cost;

        GameObject _turret = (GameObject)Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        //Debug.Log("Turret Builded");
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            //Debug.Log("Not Enough Money to upgrade");
            return;
        }

        PlayerStats.Money -= turretBluePrint.upgradeCost;

        //Delete old turret
        Destroy(turret);

        //Build new turret
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        //Debug.Log("Turret Upgraded");
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(turret);
        turretBluePrint = null;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    
}
