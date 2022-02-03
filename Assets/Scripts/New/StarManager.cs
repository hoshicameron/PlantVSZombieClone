using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarManager : SingletonMonoBehaviour<StarManager>
{
    [SerializeField] private int startingStars = 100;
    private int currentStars;

    // Start is called before the first frame update
    void Start()
    {
        currentStars = startingStars;
        EventHandler.CallUpdateStarsEvent(currentStars);
    }

    public bool UseStars(int amount)
    {
        if (currentStars < amount) return false;

        currentStars -= amount;
        EventHandler.CallUpdateStarsEvent(currentStars);
        return true;

    }

    public void AddStar(int amount)
    {
        currentStars += amount;
        EventHandler.CallUpdateStarsEvent(currentStars);
    }
}
