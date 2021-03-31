using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class Enemy : MonoBehaviour
{
    #region CodeForGame
    public int EnemyType = 1;    
    public float startHealth = 0f;    
    public float startSpeed = 0f;    
    private int gainMoney = 0;

    [HideInInspector]
    public float health;
    [HideInInspector]
    public float speed;

    public GameObject deathEffect;
    private bool isDead = false;

    [Header("UnityStuff")]
    public Image healthBar;
    
    string configFileDataName;
    #endregion

    private void Awake()
    {
        configFileDataName = "EnemyConfig.csv";
        SetConfig();
    }

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
                
    }

    #region CSVpart
    private void SetConfig()
    {
        string path = Application.dataPath + "/StreamingAssets";
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(path, configFileDataName));
            string type = input.ReadLine();
            string values = input.ReadLine();
            while(values != null)
            {
                AssignData(values);
                values = input.ReadLine();
            }
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            if(input != null)
            {
                input.Close();
            }
        }
    }
    private void AssignData(string values)
    {
        string[] data = values.Split(',');
        if (EnemyType == int.Parse(data[1]))
        {
            startHealth = float.Parse(data[2]);
            startSpeed = float.Parse(data[3]);
            gainMoney = int.Parse(data[4]);
        }
    }
    #endregion

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth ;

        if(health <=0 && !isDead)
        {
            Die();
        }

    }
    void Die()
    {
        isDead = true;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        GainMoney();
        WaveSpawner.EnemyAlives--;
        Destroy(gameObject);
    }

    void GainMoney()
    {
        PlayerStats.Money += gainMoney;
        //PlayerStats.AddMoney(gainMoney);        
    }
    
    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);
    }    
}
