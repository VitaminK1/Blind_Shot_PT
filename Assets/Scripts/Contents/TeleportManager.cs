using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform _inGamePlayerTransform;
    [SerializeField] private Transform _gameOverPlayerTransform;

    private void Awake()
    {
        GameManager.OnGameStateChangedAction += TeleportPlayer;
    }

    private void TeleportPlayer(Define.GameState gameState)
    {
        switch (gameState)
        {
            case Define.GameState.InGame:
                TeleportPlayer(_inGamePlayerTransform);
                break;
            case Define.GameState.GameOver:
                TeleportPlayer(_gameOverPlayerTransform);
                break;
        }
    }

    public void TeleportPlayer(Transform trans)
    {
        Player.Instance.transform.SetPositionAndRotation(trans.position, trans.rotation);
    }
}
