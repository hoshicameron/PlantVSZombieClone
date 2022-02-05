using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<GameObject,int> DefenderSelectionEvent;

    public static void CallDefenderSelectionEvent(GameObject defender,int cost)
    {
        DefenderSelectionEvent?.Invoke(defender,cost);
    }

    public static event Action<int> UpdateStarsEvent;

    public static void CallUpdateStarsEvent(int amount)
    {
        UpdateStarsEvent?.Invoke(amount);
    }

    public static event Action EndGameEvent;

    public static void CallEndGameEvent()
    {
        EndGameEvent?.Invoke();
    }


}
