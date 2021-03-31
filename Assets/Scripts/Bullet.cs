using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    protected float speed = 0f;
    [SerializeField]
    protected int damage = 0;
    [SerializeField]
    protected float explosionRadius = 0f;
    public int bulletType;

    public GameObject impactEffect;

    string configFileDataName;
    
    private void Awake()
    {
        configFileDataName = "BulletConfig.csv";
        SetConfig();
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
        if (bulletType == int.Parse(data[1]))
        {
            damage = int.Parse(data[2]);
            speed = float.Parse(data[3]);
            explosionRadius = float.Parse(data[4]);
        }
    }

    public void Seeking(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        BulletToTarget();
    }
    void BulletToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }
    virtual protected void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 5f);
        
        Damage(target);
        //Debug.Log("Hit");
               
        Destroy(gameObject);
               
    }
    protected void Damage(Transform enemy)
    {
        Enemy _enemy = enemy.GetComponent<Enemy>();
        if (_enemy != null)
        {
            _enemy.TakeDamage(damage);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
 