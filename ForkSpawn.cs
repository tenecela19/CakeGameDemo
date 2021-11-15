using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkSpawn : MonoBehaviour
{
    public GameObject Fork;
    public float speed;
    public Transform[] Waypoints;
    GameObject obj;
    Rigidbody _rigfork;
    public Vector3 offset;
    public Vector3[] PositionOffset;
    int TypeFork;
    void Start()
    {
        TypeFork = Random.Range(0, Waypoints.Length + 1);
      //  TypeFork = 4;
        StartCoroutine(SpawingWaveFork());

    }

    IEnumerator SpawingWaveFork()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,2f)); 

        switch (TypeFork)
        {
            case 0:
                obj = Instantiate(Fork, Waypoints[0].position, Quaternion.Euler(0, 0, 135));
                break;
            case 1:
                obj = Instantiate(Fork, Waypoints[1].position, Quaternion.Euler(0, 0, 240));
                break;
            case 2:
                obj = Instantiate(Fork, Waypoints[2].position, Quaternion.Euler(0, 90, 225));

                break;
            case 3:
                obj = Instantiate(Fork, Waypoints[3].position, Quaternion.Euler(0, 30, 225));
                break;
            case 4:
                obj = Instantiate(Fork, Waypoints[4].position, Quaternion.Euler(0, -30, 135));
                break;
            default:
                obj = Instantiate(Fork, Waypoints[0].position, Quaternion.Euler(0, 0, 145));
                break;
        }
        _rigfork = obj.GetComponent<Rigidbody>();

    }


    public void FixedUpdate()
    {
        if (obj != null)
        {

            switch (TypeFork)

            {
                case 0:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[0], Time.deltaTime * speed));
                    // Distance();
                    break;
                case 1:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[1], Time.deltaTime * speed));
                    //Distance();
                    break;
                case 2:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[2], Time.deltaTime * speed));
                    //   Distance();
                    break;
                case 3:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[3], Time.deltaTime * speed));
                    //   Distance();
                    break;
                case 4:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[4], Time.deltaTime * speed));
                    //   Distance();
                    break;
                default:
                    _rigfork.MovePosition(Vector3.MoveTowards(obj.transform.position, gameObject.transform.position + PositionOffset[0], Time.deltaTime * speed));
                    //    Distance();
                    break;
            }
        }



    }
}
