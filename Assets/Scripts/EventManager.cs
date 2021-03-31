using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static WaveSpawner invoker;
    static UnityAction<int> listener;

    public static void AddInvoker(WaveSpawner script)
    {
        invoker = script;
        if(listener != null)
        {
            invoker.EventHandleEventAddListener(listener);
        }
    }
    public static void AddListener(UnityAction<int> handler)
    {
        listener = handler;
        if(invoker != null)
        {
            invoker.EventHandleEventAddListener(listener);
        }
    }
} 


