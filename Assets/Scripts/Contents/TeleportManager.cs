using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform _inGamePlayerTransform;
    [SerializeField] private Transform _WeaponTransform;
    [SerializeField] private Transform _gameOverPlayerTransform;
    [SerializeField] private Transform _gameEndingTransform;

    private void Awake()
    {
        GameManager.OnGameStateChangedAction += TeleportSetup;
    }

    private void TeleportSetup(Define.GameState gameState)
    {
        switch (gameState)
        {
            case Define.GameState.InGame:
                TeleportPlayer(_inGamePlayerTransform);
                TeleportWeapon(_WeaponTransform);
                break;
            case Define.GameState.GameOver:
                TeleportPlayer(_gameOverPlayerTransform);
                TeleportWeapon(_WeaponTransform);
                break;
            case Define.GameState.Ending:
                TeleportPlayer(_gameEndingTransform);
                TeleportWeapon(_WeaponTransform);
                break;
        }
    }

    public void TeleportPlayer(Transform trans)
    {
        Player.Instance.transform.SetPositionAndRotation(trans.position, trans.rotation);
    }

    private void TeleportWeapon(Transform trans) 
    {
        Weapon.Instance.transform.SetPositionAndRotation(trans.position, trans.rotation);
    }
}
