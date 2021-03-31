using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    #region CodeForGame
    public static int EnemyAlives = 0;
    public Wave[] waves;

    public Transform SpawnPoint;
    
    public float TimeBetweenWaves = 5.5f;
    private float Countdown = 2f;

    public Text WaveCountdownText;

    private int WaveIndex = 0;    

    public GameManager gameManager;
    #endregion

    #region CodeForEventHandle
    public int roundSurvived = 1;
    EventHandle eventHandle = new EventHandle();
    public void EventHandleEventAddListener(UnityAction<int> listener)
    {
        eventHandle.AddListener(listener);
    }
    #endregion

    #region CodeForEventManager
    private void Start()
    {

        EventManager.AddInvoker(this);
    
    }
    #endregion
    private void Update()
    {
        EnemyAlive();
    }

    private void OnEnable()
    {
        EnemyAlives = 0;
    }
    
    public void EnemyAlive()
    {
        if (EnemyAlives > 0)
        {
            return;
        }

        if (WaveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (Countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            Countdown = TimeBetweenWaves;
            return;
        }        
        Countdown -= Time.deltaTime;
        Countdown = Mathf.Clamp(Countdown, 0f, Mathf.Infinity);
        WaveCountdownText.text = "Next wave in : " + string.Format("{0:00.00}", Countdown);
    }

    IEnumerator SpawnWave()
    {
        eventHandle.Invoke(roundSurvived);
        //PlayerStats.IncreaseRoundSurvive(roundSurvived);

        Wave wave = waves[WaveIndex];

        EnemyAlives = wave.count + wave.count2 + wave.count3;

        //Debug.Log("Wave Incomeing!");
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);            
        }

        for (int i = 0; i < wave.count2; i++)
        {
            SpawnEnemy(wave.enemyPrefab2);
            yield return new WaitForSeconds(1f / wave.rate2);            
        }
        for (int i = 0; i < wave.count3; i++)
        {
            SpawnEnemy(wave.enemyPrefab3);
            yield return new WaitForSeconds(1f / wave.rate3);
        }
        WaveIndex++;
    }
    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
        //EnemyAlives++;
    }
    
}
