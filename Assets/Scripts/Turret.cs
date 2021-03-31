using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targrtEnemy;

    public int TurretType = 1;

    [Header("Universal Setup")]
    public float range = 0f;
    public float fireRate = 0f;
    public float turnSpeed = 0f; 
    private float fireCountDown = 0f;

    [Header("Laser Turret")]
    public bool useLaser = false;
    public int damageOvertime = 5;
    public float slowAmount = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public Transform firePoint;

    string configFileDataName;

    private void Awake()
    {
        configFileDataName = "TurretConfig.csv";
        SetConfig();
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);        
    }

    private void SetConfig()
    {
        string path = Application.dataPath + "/StreamingAssets";
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(path, configFileDataName));
            string type = input.ReadLine();
            string values = input.ReadLine();
            while (values != null)
            {
                AssignData(values);
                values = input.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }
    private void AssignData(string values)
    {
        string[] data = values.Split(',');
        if (TurretType == int.Parse(data[1]))
        {
            range = float.Parse(data[2]);
            fireRate = float.Parse(data[3]);
            turnSpeed = float.Parse(data[4]);
            damageOvertime = int.Parse(data[5]);
            slowAmount = float.Parse(data[6]);
        }
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        //For Select Use Between 1&2
        // 1.)
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targrtEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        // 2.) Broken
        //if(target != null && Vector3.Distance(transform.position,target.position)<=range)
        //{
        //    return;
        //}

        else
        {
            target = null;
        }

    }

    void Update()
    {
        if (target == null)
        {
            if(useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                    
            }
            return;
        }
            
        if (useLaser)
        {
          Laser();
        }
        else
        {
            TurretShooting();
        }

        TurretLockOnTarget();
               
    }
    void TurretLockOnTarget()
    {
        //Lock On Target
        Vector3 dirction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dirction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

   void TurretShooting()
   {
        if(fireCountDown <= 0f)
        {
            Shooting();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
   }
   void Shooting()
   {
        //Debug.Log("Shoot!!");
        GameObject bulletGameobject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameobject.GetComponent<Bullet>();
        if (bullet != null)
            bullet.Seeking(target);
   }

    void Laser()
    {
        targrtEnemy.TakeDamage(damageOvertime * Time.deltaTime);
        targrtEnemy.Slow(slowAmount);
      
        //graphic Part
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
            
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;


        impactEffect.transform.position = target.position + dir.normalized * 0.5f;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
        
}
