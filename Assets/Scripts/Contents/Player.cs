using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private PlayerLifeController m_LifeController;
    
    public event Action<int> OnHit;

    public void Hit(int damage)
    {
        if (m_LifeController.LifeState == Define.PlayerLifeState.Dead || 
            GameManager.Instance.CurrentGameState != Define.GameState.InGame) return;
        
        OnHit?.Invoke(damage);
    }
}
