using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool isGamePaused = false;
    private bool isGameEnded = false;

    public bool IsGameEnded
    {
        get { return isGameEnded; }
        set { isGameEnded = value; }
    }

    public bool IsGamePaused
    {
        get { return isGamePaused; }
        set { isGamePaused = value; }
    }

    public override void Awake()
    {
        base.Awake();

        isGameEnded = false;
        isGamePaused = false;
    }


}
