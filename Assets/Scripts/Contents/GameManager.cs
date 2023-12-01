using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] protected int _wave = 0;
    public int Wave
    {
        get { return _wave; }
        set { _wave = Mathf.Max(0, value); }
    
    }
    
    [Header("Player")]
    [SerializeField] private GameObject m_Player;
    public GameObject Player => m_Player;
    
    static GameManager _instance;
    public static GameManager Instance => _instance;
    
    private Define.GameState m_CurrentGameState = Define.GameState.None;
    public Define.GameState CurrentGameState
    {
        get { return m_CurrentGameState; }
        set
        {
            if (m_CurrentGameState != value) return;
            
            m_CurrentGameState = value;
            OnGameStateChanged();
        }
    }

    public event Action<Define.GameState> OnGameStateChangedAction;
    public event Action OnInGameStart;
    public event Action OnGameOver;
    public event Action OnEnding;
    
    void Start()
    {
        ChangeGameState(Define.GameState.InGame);
    }

    private void ChangeGameState(Define.GameState gameState)
    {
        CurrentGameState = gameState;
    }

    private void OnGameStateChanged()
    {
        switch (CurrentGameState)
        {
            case Define.GameState.InGame:
                OnInGameStart?.Invoke();
                break;
            case Define.GameState.GameOver:
                OnGameOver?.Invoke();
                break;
            case Define.GameState.Ending:
                OnEnding?.Invoke();
                break;

        }
        
        OnGameStateChangedAction?.Invoke(CurrentGameState);
    }
}
