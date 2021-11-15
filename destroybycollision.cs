using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroybycollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player") || other.transform.CompareTag("Enemy"))
        Destroy(other.gameObject);
    }
}
