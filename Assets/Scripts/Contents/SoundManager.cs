using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _bgmAudioSource;
    
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    private void Awake()
    {
        GameManager.OnGameStateChangedAction += OnGameStateChanged;
    }

    public void PlayBgm()
    {
        _bgmAudioSource.loop = true;
        _bgmAudioSource.spatialBlend = 0;
        _bgmAudioSource.Play();
    }

    private void StopBgm()
    {
        _bgmAudioSource.Stop();
    }

    private void OnGameStateChanged(Define.GameState gameState)
    {
        if (gameState == Define.GameState.InGame)
        {
            PlayBgm();
        }
        else
        {
            StopBgm();
        }
    }
}

