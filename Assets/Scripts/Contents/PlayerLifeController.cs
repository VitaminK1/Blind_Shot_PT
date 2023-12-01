using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private int m_DefaultHealth = 100;

    private Define.PlayerLifeState _lifeState = Define.PlayerLifeState.Alive;
    public Define.PlayerLifeState LifeState => _lifeState;
    
    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (value <= 0)
            {
                _lifeState = Define.PlayerLifeState.Dead;
                GameManager.Instance.ChangeGameState(Define.GameState.GameOver);
            }
        }
    }
    
    private void Awake()
    {
        GameManager.OnGameStateChangedAction += OnGameStateChanged;
    }

    private void Start()
    {
        Player.Instance.OnHit += DecreaseHealth;
    }

    public void DecreaseHealth(int damage)
    {
        CurrentHealth -= damage;
    }

    private void OnGameStateChanged(Define.GameState state)
    {
        if (state == Define.GameState.InGame)
        {
            _lifeState = Define.PlayerLifeState.Alive;
            CurrentHealth = m_DefaultHealth;
        }
    }
    
}
