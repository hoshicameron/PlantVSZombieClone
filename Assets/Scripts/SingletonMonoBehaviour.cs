using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> :MonoBehaviour where T:MonoBehaviour
{
    public static T Instance { get; set; }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        } else
        {
            Destroy(gameObject);
        }
    }
}
