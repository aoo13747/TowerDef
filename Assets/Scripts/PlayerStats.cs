using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 100;

    public static int Lives;
    public int startLives = 5;

    public static int Rounds;


    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;

        #region CodeForEventHandle
        //WaveSpawner waveSpawnerScript = GetComponent<WaveSpawner>();
        //waveSpawnerScript.EventHandleEventAddListener(IncreaseRoundSurvive);
        #endregion
        #region CodeForEventManager
        EventManager.AddListener(IncreaseRoundSurvive);
        #endregion
    }
    public static void AddMoney(int money)
    {
        Money += money;
    }
    public static void LiveDecrease(int live)
    {
        Lives -= live;
    }
    public void IncreaseRoundSurvive(int round)
    {
        Rounds += round;
    }
}
