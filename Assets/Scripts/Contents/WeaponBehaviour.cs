using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private float reloadTime = 0.5f; // time to be enabled after being inactive
    [SerializeField]
    private float activeDuration = 0.5f; // time to be active after use
    [SerializeField]
    private GameObject go;
    private bool isEnabled = true;

    public void Activate() {
        if (!isEnabled) return;
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot() {

        isEnabled = false;
        go.SetActive(true);
        yield return new WaitForSeconds(activeDuration);
        go.SetActive(false);
        yield return new WaitForSeconds(reloadTime);
        isEnabled = true;
    }

    

}
