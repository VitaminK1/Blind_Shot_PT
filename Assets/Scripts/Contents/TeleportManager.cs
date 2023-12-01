using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private Transform m_InGamePlayerTransform;
    [SerializeField] private Transform m_GameOverPlayerTransform;

    private void Awake()
    {
        GameManager.Instance.OnGameStateChangedAction += TeleportPlayer;
    }

    private void TeleportPlayer(Define.GameState gameState)
    {
        switch (gameState)
        {
            case Define.GameState.InGame:
                TeleportPlayer(m_InGamePlayerTransform);
                break;
            case Define.GameState.GameOver:
                TeleportPlayer(m_GameOverPlayerTransform);
                break;
        }
    }

    public void TeleportPlayer(Transform trans)
    {
        if (!m_Player) return;

        m_Player.transform.position = trans.position;
        m_Player.transform.rotation = trans.rotation;
    }
}
