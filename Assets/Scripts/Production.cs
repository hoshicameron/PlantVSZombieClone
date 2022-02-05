using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    [SerializeField] private int productionAmount = 10;

    [SerializeField] private AudioSource audiosource;


    public void AddStar()
    {
        StarManager.Instance.AddStar(productionAmount);
        audiosource.Play();
    }
}
