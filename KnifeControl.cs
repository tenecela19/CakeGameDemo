using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : MonoBehaviour
{
    Rigidbody rig;
    public float Speed;
    public GameObject[] waypoints;
    public int current = 0;
    public float speed;
    public Vector3[] offset;
    public GameObject locationSpawn;
    public static int Knifepos;
    public GameObject test;
    private void Awake()
    {
        rig = gameObject.GetComponent<Rigidbody>();
    }

}
