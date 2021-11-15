using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick moveJoystick;
    public FixedJoystick lookJoystick;
    public Animator anim;
    Rigidbody rig;
    public float speed;
    public static bool _ismoving;
    bool StopMoving = false;
    public float speedTimer = 2;
    public AudioSource walking;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();

    }
    public void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && CakeControlller.GameOver == false)
        {
            anim.SetBool("Winner", true);
            anim.SetBool("dead", false);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(StopMoving == false)
        {
            UpdateMoveJoystick();
            UpdateLookJoystick();
        }

    }

    void UpdateMoveJoystick()
    {
        anim.SetBool("walking", _ismoving);
    
        float hoz = moveJoystick.Horizontal;
       float ver = moveJoystick.Vertical;
        Vector2 convertedXY = ConvertWithCamera(Camera.main.transform.position, hoz, ver);
        Vector3 direction = new Vector3(convertedXY.x, 0, convertedXY.y).normalized;
        transform.Translate(direction * speed, Space.World);
 
    }
    void UpdateLookJoystick()
    {
        float hoz = lookJoystick.Horizontal;
        float ver = lookJoystick.Vertical;
        Vector2 convertedXY = ConvertWithCamera(Camera.main.transform.position, hoz, ver);
        Vector3 direction = new Vector3(convertedXY.x, 0, convertedXY.y).normalized;
        Vector3 lookAtPosition = transform.position + direction;
        transform.LookAt(lookAtPosition);
    }


    private Vector2 ConvertWithCamera(Vector3 cameraPos, float hor, float ver)
    {
        Vector2 joyDirection = new Vector2(hor, ver).normalized;
        Vector2 camera2DPos = new Vector2(cameraPos.x, cameraPos.z);
        Vector2 playerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 cameraToPlayerDirection = (Vector2.zero - camera2DPos).normalized;
        float angle = Vector2.SignedAngle(cameraToPlayerDirection, new Vector2(0, 1));
        Vector2 finalDirection = RotateVector(joyDirection, -angle);
        return finalDirection;
    }

    public Vector2 RotateVector(Vector2 v, float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Knifes"))
        {
          anim.SetBool("dead", true);
            StopMoving = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("PieceBye"))
        {
            StopMoving = true;
            FixedJoint con = gameObject.AddComponent<FixedJoint>();
            con.connectedBody = collision.transform.GetComponent<Rigidbody>();
            CakeControlller.GameOver = true;
        }
        if (collision.transform.CompareTag("Fork"))
        {
           if(collision.transform.GetComponent<ForkControl>().KillPlayer == true)
            {
                StopAllCoroutines();
                anim.SetBool("dead", true);
                rig.isKinematic = true;
                StopMoving = true;
            }

        }

    }
   public void PlayerHasDead()
    {
        CakeControlller.GameOver = true;
    }
    public void PlayerHasWin()
    {
        CakeControlller.Instance.Winner = true;
    }
    public void Walkking()
    {
        walking.Play();
    }
}
