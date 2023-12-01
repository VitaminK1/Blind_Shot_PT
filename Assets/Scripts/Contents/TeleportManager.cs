using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform m_InGamePlayerTransform;
    [SerializeField] private Transform m_GameOverPlayerTransform;

    private void Awake()
    {
        GameManager.OnGameStateChangedAction += TeleportPlayer;
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
        GameManager.Instance.Player.transform.position = trans.position;
        GameManager.Instance.Player.transform.rotation = trans.rotation;
    }
}
