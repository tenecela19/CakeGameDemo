using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{
    public float movespeed = 3;
    public float rotspeed = 100f;


    public bool iswandering = false;
    bool isrotationR = false;
    bool isrotationL = false;
    private bool iswalking = false;
    Animator anim;
    Rigidbody rig;
    bool HasDead;
    public AudioSource walking;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(transform.up * Random.Range(45, 90));


    }
    void WanderIA()
    {

        if (iswandering == false)
        {
            StartCoroutine(wandering());
        }
        if (isrotationR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotspeed);
        }
        if (isrotationL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotspeed);

        }
        if (iswalking == true)
        {
            transform.position += transform.right * movespeed * Time.deltaTime;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (CakeControlller.GameOver == false )
        {
            if(HasDead == false)
            {
                WanderIA();
            }
        }
       

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("red") )
        {
            // iswandering = true;
            transform.rotation *= Quaternion.Euler(0, 180, 0);

        }
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Fork"))
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
        if (collision.transform.CompareTag("PieceBye") )
        {
            transform.tag = "Finish";
            HasDead = true;
            StopAllCoroutines();
            FixedJoint con = gameObject.AddComponent<FixedJoint>();
            con.connectedBody = collision.transform.GetComponent<Rigidbody>();
            Destroy(gameObject, 2);
        }
        if (collision.transform.CompareTag("Fork"))
        {
            if (collision.transform.GetComponent<ForkControl>().KillPlayer == true)
            {
                transform.tag = "Finish";

                StopAllCoroutines();
                HasDead = true;
                rig.isKinematic = true;
                anim.SetBool("Dead", true);
                Destroy(gameObject, 2);

            }

        }
    }
    IEnumerator wandering()
    {
        bool WalkKnoworNot()
        {
            if (Random.value > 0.5f)
            {
                return true;
            }
            else return false;
        }
        if (WalkKnoworNot() == true){
            int rottime = Random.Range(1, 3);
            float rotateWait = Random.Range(0.05f,0.2f);
            int rotateLR = Random.Range(1, 3);
            int walkwait = Random.Range(1, 4);
            int walktime = Random.Range(1, 2);
            iswandering = true;

            yield return new WaitForSeconds(Random.Range(0,0.2f));
            iswalking = true;
            anim.SetBool("Iswalking", true);
            yield return new WaitForSeconds(walktime);
            iswalking = false;
            anim.SetBool("Iswalking", false);

            yield return new WaitForSeconds(0.1f);
            if (rotateLR == 1)
            {
                isrotationR = true;
                yield return new WaitForSeconds(rotateWait);
                isrotationR = false;
            }
            if (rotateLR == 2)
            {
                isrotationL = true;
                yield return new WaitForSeconds(0.1f);
                isrotationL = false;
            }
            iswandering = false;
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Knifes"))
        {
            transform.tag = "Finish";
            StopAllCoroutines();
            HasDead = true;
            rig.isKinematic = true;
            anim.SetBool("Dead", true);
            Destroy(gameObject, 2);
        }
    }

}
