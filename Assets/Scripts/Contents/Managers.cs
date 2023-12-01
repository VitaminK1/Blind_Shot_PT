using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers
    : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null) 
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
            _instance._pool.Init();
        }
    }
    public static void Clear() {
        Pool.Clear();
        return;
    }
}
