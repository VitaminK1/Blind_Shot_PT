using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _reloadTime = 0.5f; // time to be enabled after being inactive
    [SerializeField]
    private float _activeDuration = 0.5f; // time to be active after use
    [SerializeField]
    private GameObject _go;
    
    private bool _isEnabled = true;

    public void Activate() {
        if (!_isEnabled) return;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot() {

        _isEnabled = false;
        _go.SetActive(true);
        yield return new WaitForSeconds(_activeDuration);
        _go.SetActive(false);
        yield return new WaitForSeconds(_reloadTime);
        _isEnabled = true;
    }

    

}
