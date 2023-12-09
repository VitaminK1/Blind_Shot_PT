using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAudioScript : MonoBehaviour
{
    [SerializeField] private AudioSource _movingSound1 = null;
    [SerializeField] private AudioSource _movingSound2 = null;
    [SerializeField] private AudioSource _movingSound3 = null;
    // Start is called before the first frame update
    void Start()
    {
        _movingSound1 = GetComponent<AudioSource>();
        _movingSound2 = GetComponent<AudioSource>();
        _movingSound3 = GetComponent<AudioSource>();
        Invoke("playMovingSound1", 1.0f);
        Invoke("playMovingSound2", 2.0f);
        Invoke("playMovingSound3", 3.0f);
    }

    void playMovingSound1()
    {
        _movingSound1.Play();
    }
    
    void playMovingSound2()
    {
        _movingSound2.Play();
    }
    
    void playMovingSound3()
    {
        _movingSound3.Play();
    }
}
