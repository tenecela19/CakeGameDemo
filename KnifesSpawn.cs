using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifesSpawn : MonoBehaviour
{

    public GameObject Knife;
    public float speed;
    public Vector2[] PositionKnifes;
    GameObject obj;
     Rigidbody rig;
    int[] Rot = { -135, -90, -45, 0, 90, 45, 135, 180 }; 
    void Start()
    {
        Invoke("InstatiateKnifes", 0.5f);
    }

    void InstatiateKnifes()
    {
        obj = Instantiate(Knife, spawn(), Quaternion.Euler(transform.localEulerAngles.x,transform.localEulerAngles.y,90));
        FindObjectOfType<AudioManager>().Play("Knive");
        rig = obj.GetComponent<Rigidbody>();
        
    }
    Vector3 spawn()
    {
        int angle = (int) transform.eulerAngles.y;

        switch (angle)
        {
            case 270:
                Vector3 mspawn90 = new Vector3(transform.localPosition.x , transform.position.y, transform.localPosition.z+10);
                return mspawn90;
            case 225:
                Vector3 Mspawn135m = new Vector3(transform.localPosition.x - 10, transform.position.y, transform.localPosition.z + 10);
                return Mspawn135m;
            case 315:
                Vector3 Mspawn45 = new Vector3(transform.localPosition.x + 10, transform.position.y, transform.localPosition.z + 10);
                return Mspawn45;
            case 0:
                Vector3 spawn0 = new Vector3(transform.localPosition.x+ 10, transform.position.y, transform.localPosition.z );
                return spawn0; 
            case 45:                
                Vector3 spawn45 = new Vector3(transform.localPosition.x + 10, transform.position.y, transform.localPosition.z - 10);
                return spawn45;
            case 90:
               Vector3 spawn90 = new Vector3(transform.localPosition.x , transform.position.y, transform.localPosition.z - 10);
                return spawn90;
            case 135:
                Vector3 spawn135 = new Vector3(transform.localPosition.x - 10, transform.position.y, transform.localPosition.z - 10);
                return spawn135;
            case 180:
                Vector3 spawn180 = new Vector3(transform.localPosition.x -10, transform.position.y, transform.localPosition.z );
                return spawn180;
                default:
                return new Vector3(transform.localPosition.x +10, transform.position.y, transform.localPosition.z+10);
        }


    }
    private void FixedUpdate()
    {
        if(obj != null)
        {
            rig.velocity = Movement();
        }
      
    }
   Vector3 Movement()
    {

        int angle = (int)transform.eulerAngles.y;
        switch (angle)
        {
            case 270:
                return new Vector3(0, 0, -1) * Time.deltaTime * speed;
            case 225:

                return new Vector3(1, 0, -1) * Time.deltaTime * speed;
            case 315:
                return new Vector3(1, 0, 1) * Time.deltaTime * -speed;
            case 0:
                return new Vector3(1, 0, 0) * Time.deltaTime *- speed;
            case 45:
                return new Vector3(-1, 0, 1) * Time.deltaTime * speed;
            case 90:
                return new Vector3(0, 0, 1) * Time.deltaTime * speed;
            case 135:
                return new Vector3(1, 0, 1) * Time.deltaTime * speed;
            case 180:
                return new Vector3(1, 0, 0) * Time.deltaTime * speed;
            default:
                return new Vector3(1, 0, 1) * Time.deltaTime * speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Knifes") && obj.transform == other.transform)
        {
            Destroy(gameObject);
        }
    }
}
