using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkControl : MonoBehaviour
{
    [HideInInspector]
    public bool KillPlayer = true;


    public GameObject[] waypoints;
    public GameObject SpawnLocation;

    private void Start()
    {


    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("PieceBye"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("PieceBye") || collision.transform.CompareTag("red"))
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("SpawPointFork"))
        {
            FindObjectOfType<AudioManager>().Play("Fork");
            KillPlayer = false;
            Rigidbody rig = gameObject.GetComponent<Rigidbody>();
            rig.isKinematic = true;
            Destroy(other.gameObject);
        }
    }
}
