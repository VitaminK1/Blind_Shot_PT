using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (!enabled)
            return;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
    
        Instance = (T)this;
    }
    
    protected void OnEnable()
    {
        if (Instance != this)
            Awake();
    }
}
