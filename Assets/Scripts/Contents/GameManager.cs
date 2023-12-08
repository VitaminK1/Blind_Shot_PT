using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyTypeCount
{
    public Define.EnemyType Type;
    public int Count;
}

[Serializable]
public class EnemySettings
{
    public List<EnemyTypeCount> EnemyTypeCounts = new List<EnemyTypeCount>();
}

public class GameManager : Singleton<GameManager>
{
    [Header("Wave Settings")]
    [SerializeField] protected List<EnemySettings> _enemyWaveSettings = new List<EnemySettings>();
    public List<EnemySettings> EnemyWaveSettings => _enemyWaveSettings;

    protected int _currentWave = 0;
    public int CurrentWave => _currentWave;

    private Define.GameState _currentGameState = Define.GameState.None;
    public Define.GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            if (_currentGameState == value) return;
            
            _currentGameState = value;
            OnGameStateChanged();
        }
    }


    public static event Action<Define.GameState> OnGameStateChangedAction;
    
    private void Start()
    {
        ChangeGameState(Define.GameState.InGame);
    }

    public void ChangeGameState(Define.GameState gameState)
    {
        if (gameState == Define.GameState.InGame)
        {
            _currentWave = 0;
        }
        
        CurrentGameState = gameState;
    }
    
    public void ProceedWave()
    {
        _currentWave++;
    }

    private void OnGameStateChanged()
    {
        OnGameStateChangedAction?.Invoke(CurrentGameState);
    }

    public void GameStateToInGameState() {
        ChangeGameState(Define.GameState.InGame);
    }
}
