using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out BaseMonsterController monster))
            {
                monster.AttackedByPlayer();
            }
        }
    }
}
