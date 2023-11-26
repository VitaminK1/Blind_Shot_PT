using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnHeight = 0;
    [SerializeField]
    float _spawnTime = 5.0f;

    private bool isSpawning = false;
    private GameObject[] _spawnPoints;

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    void Start()
    {
        _spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while (!isSpawning && _reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine(ReserveSpawn());
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        isSpawning = true;
        yield return new WaitForSeconds(_spawnTime);
        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Enemy, "Enemy");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();
        if (_spawnPoints.Length < 1)
        {
            obj.transform.position = new Vector3(0, 0, 0);
        }
        else 
        {
            obj.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)].transform.position + new Vector3(0,_spawnHeight, 0);
        }
        _reserveCount--;
        isSpawning = false;
    }
}
