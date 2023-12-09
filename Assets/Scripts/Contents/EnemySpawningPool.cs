using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawningPool : Singleton<EnemySpawningPool>
{
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private BaseMonsterController _flyingEnemy;
    [SerializeField] private BaseMonsterController _walkingEnemy;

    [SerializeField] private Transform _parent;
    [SerializeField] float _spawnHeight = 0;
    [SerializeField] float _spawnTime = 5.0f;

    private HashSet<BaseMonsterController> _monsters = new HashSet<BaseMonsterController>();
    private CancellationTokenSource _cancellationTokenSource;

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnGameStateChangedAction += OnGameStateChanged;
    }

    private void OnGameStateChanged(Define.GameState gameState)
    {
        switch (gameState)
        {
            case Define.GameState.InGame:
                StartEnemySpawning();
                break;
            case Define.GameState.GameOver:
                StopEnemySpawning();
                break;
            case Define.GameState.Ending:
                StopEnemySpawning();
                break;
        }
    }

    void StartEnemySpawning()
    {
        if (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;

            foreach (var monster in _monsters)
            {
                if (monster != null)
                {
                    Destroy(monster.gameObject);
                }
            }
            _monsters.Clear();
        }

        _cancellationTokenSource = new CancellationTokenSource();
        SpawnWave(_cancellationTokenSource.Token).Forget();
    }

    void StopEnemySpawning()
    {
        if (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = null;
        }
    }

    async UniTaskVoid SpawnWave(CancellationToken cancellationToken)
    {
        GameManager gameManager = GameManager.Instance;

        while (gameManager.CurrentWave < gameManager.EnemyWaveSettings.Count)
        {
            EnemySettings currentWaveSettings = gameManager.EnemyWaveSettings[gameManager.CurrentWave];


            BaseMonsterController monster = SpawnEnemy((Define.EnemyType)Random.Range(0, 2), _parent);
            await UniTask.Delay(TimeSpan.FromSeconds(_spawnTime), cancellationToken: cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    bool AllEnemiesDefeated(List<BaseMonsterController> enemies)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }

        return true;
    }

    BaseMonsterController SpawnEnemy(Define.EnemyType enemyType, Transform parent)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Quaternion spawnRotation = Quaternion.identity;

        BaseMonsterController monster = null;
        if (spawnPosition.y > _spawnHeight)
        {
            monster = InstantiateEnemy(Define.EnemyType.Flying, spawnPosition, spawnRotation, parent);
        }
        else
        {
            monster = InstantiateEnemy(Define.EnemyType.Walking, spawnPosition, spawnRotation, parent);
        }
        _monsters.Add(monster);

        //Debug.Log("spawned enemy");
        return monster;
    }

    Vector3 GetRandomSpawnPosition()
    {
        if (_spawnPoints.Length < 1)
        {
            return new Vector3(0, _spawnHeight, 0);
        }
        else
        {
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length - 1)];
            return spawnPoint.position + new Vector3(0, _spawnHeight, 0);
        }
    }

    BaseMonsterController InstantiateEnemy(Define.EnemyType enemyType, Vector3 position, Quaternion rotation)
    {
        switch (enemyType)
        {
            case Define.EnemyType.Flying:
                return Instantiate(_flyingEnemy, position, rotation);
            case Define.EnemyType.Walking:
                return Instantiate(_walkingEnemy, position, rotation);
            default:
                return null;
        }
    }

    BaseMonsterController InstantiateEnemy(Define.EnemyType enemyType, Vector3 position, Quaternion rotation, Transform parent)
    {
        switch (enemyType)
        {
            case Define.EnemyType.Flying:
                return Instantiate(_flyingEnemy, position, rotation, parent);
            case Define.EnemyType.Walking:
                return Instantiate(_walkingEnemy, position, rotation, parent);
            default:
                return null;
        }
    }

    public void OnEnemyDespawn(BaseMonsterController monster)
    {
        _monsters.Remove(monster);
    }
}
