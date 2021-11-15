using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float Timer;
    void Start()
    {
        Destroy(gameObject, Timer);
    }
}
