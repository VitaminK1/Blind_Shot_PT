using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject m_FlyingEnemy;
    [SerializeField] private GameObject m_WalkingEnemy;
    
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject SpawnEnemy(Define.EnemyType enemyType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject obj = null;
        
        switch (enemyType)
        {
            case Define.EnemyType.Flying:
                obj = Instantiate(m_FlyingEnemy, position, rotation);
                _monsters.Add(obj);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                return obj;
            case Define.EnemyType.Walking:
                obj = Instantiate(m_WalkingEnemy, position, rotation);
                _monsters.Add(obj);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                return obj;
        }

        return obj;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        if (go.CompareTag("Enemy")) return Define.WorldObject.Enemy;
        else if (go.CompareTag("Player")) return Define.WorldObject.Player;
        return Define.WorldObject.Unknown;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);
        Debug.Log(type.ToString());
        switch (type)
        {
            case Define.WorldObject.Enemy:
                {
                    if (_monsters.Contains(go))
                    {
                        _monsters.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
