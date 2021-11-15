using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovent : MonoBehaviour
{
    public GameObject Player;
    private Vector3 offset;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - Player.transform.position;

    }
    void Update()
    {
        if(Player != null)
        {
            transform.position = Player.transform.position + offset;

        }
    }
}
