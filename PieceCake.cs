using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCake : MonoBehaviour
{
    Rigidbody rig;
    public float ElevateSpeed;
    public bool ismoving;
    private void Start()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }
    void Movement()
    {
        if (transform.CompareTag("PieceBye"))
        {
            ismoving = true;
            rig.velocity = Vector3.up * ElevateSpeed;
            rig.AddForce(Vector3.up * 5);
            Destroy(gameObject, 5);
        }

    }
    private void Update()
    {
        Movement();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("red"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        }
        if (collision.transform.CompareTag("Fork"))
        {
            if(ismoving == true)
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            }
        }
    }
}
