using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    public static bool EndsAnim;
    public AudioSource ring;
    public void Start()
    {
        
    }
    public void IsAnimended()
    {
        EndsAnim = true;
        gameObject.SetActive(false);
    }
    public void RingStart()
    {
        ring.Play();
    }
}
