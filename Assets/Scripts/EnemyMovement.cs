using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{   
    private Enemy enemy;
    private Transform Target;
    private int WavePointIndex = 0;
            
    private void Start()
    {
        enemy = GetComponent<Enemy>();
        Target = WayPoints.Points[0];
    }
    private void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        Vector3 Direction = Target.position - transform.position;
        transform.Translate(Direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }

        enemy.speed = enemy.startSpeed;
    }
    void GetNextWayPoint()
    {
        if (WavePointIndex >= WayPoints.Points.Length - 1)
        {
            EndPath();
            return;
        }

        WavePointIndex++;
        Target = WayPoints.Points[WavePointIndex];
    }
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemyAlives--;
        Destroy(gameObject);
    }
}
