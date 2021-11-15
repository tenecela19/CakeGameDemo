using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeIgnoreCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("PieceBye") && gameObject.transform.CompareTag("PieceBye"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());

        }
    }

}
