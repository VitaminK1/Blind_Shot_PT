using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Weapon : Singleton<Weapon>
{
    [SerializeField]
    private float _reloadTime = 0.5f; // time to be enabled after being inactive
    [SerializeField]
    private float _activeDuration = 0.5f; // time to be active after use
    [SerializeField]
    private GameObject _go; // glass object for activating colliders
    [SerializeField]
    private AudioSource _audioSource; // audio source for shooting sound

    private CancellationTokenSource _shootCts;

    protected override void Awake()
    {
        base.Awake();
        _shootCts = new CancellationTokenSource();
        GameManager.OnGameStateChangedAction += OnGameStateChanged;
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnGameStateChanged(Define.GameState gameState)
    {
        CancelShootCts();
    }

    void CancelShootCts()
    {
        _shootCts?.Cancel();
    }

    // This method is currently registered on XR Grab Interactable Activate EventArgs
    public void Activate()
    {
        CancelShootCts();
        _shootCts = new CancellationTokenSource();
        Shoot(_shootCts.Token).Forget();
    }

    async UniTaskVoid Shoot(CancellationToken cancellationToken)
    {
        _audioSource.Play();
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            _go.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(_activeDuration), cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            _go.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(_reloadTime), cancellationToken: cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
        }
        catch (OperationCanceledException)
        {
            _go.SetActive(false);
        }
    }

    private void OnDisable()
    {
        CancelShootCts();
    }

    private void OnDestroy()
    {
        CancelShootCts();
        _shootCts.Dispose();
    }


    //private IEnumerator ShootOld() {

    //    _isEnabled = false;
    //    _go.SetActive(true);

    //    yield return new WaitForSeconds(_activeDuration);
    //    _go.SetActive(false);
    //    yield return new WaitForSeconds(_reloadTime);
    //    _isEnabled = true;
    //}

}
