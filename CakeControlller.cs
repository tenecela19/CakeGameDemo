using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CakeControlller : MonoBehaviour
{
    [Header("Cake System ")]
    public List<GameObject> PieceCake;
    public static bool GameOver;
    public Text CountdownTimer;
    public GameObject StartingUI;
    static float currentTime;
    float startingTime = 3;
    public PieceCake Piece;
    public bool Winner;
    [Header("Fork and Knifes System")]
    public bool HordeAttacks;
    public GameObject Fork;
    public GameObject Knife;
    public float ForksSpeed;
    public float ElevateSpeed;
    public Vector2[] PositionForks;
    public Vector2[] PositionKnifes;
    bool TimeToSpawn;
    public int PlayersPlaying = 4;
    public List<Transform> Players = new List<Transform>();

    [Header("Starting")]
    public GameObject[] Starting321;
    private bool StartedToPlay;
    public GameObject HowToPlayPanel;
    public GameObject Counter;
    public GameObject GameOverPanel;
    public GameObject WINNER;

    

    
    #region Singleton
    private static CakeControlller _instance;

    public static CakeControlller Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CakeControlller>();
            }

            return _instance;
        }
    }


    #endregion

    public void Awake()
    {
        _instance = this;
        Time.timeScale = 0;
        Starting.EndsAnim = false;
    }
    public void Start()
    {
         WINNER.SetActive(false);
         GameOver = false;
        Counter.SetActive(false);
        TimeToSpawn = true;
        StartedToPlay = false;
        currentTime = startingTime;
       StartCoroutine(TakeOutCakes());
       Invoke("WaitHordes", 2f);


    }
    void WaitHordes()
    {
       StartCoroutine(KnivesHorde());
       StartCoroutine(ForksHorde());
    }
    void Update()
    {
        if(Winner == true && GameOver == false)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Fork"))
            {
                Destroy(item);
            }
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("Knifes"))
            {
                Destroy(item);
            }
            StopAllCoroutines();
            Invoke("ShowWin", 1f);
        }

        if (GameOver == true)
        {
            StopAllCoroutines();
            Invoke("ShowDeath", 1f);
            Winner = false;
        }

        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameOver = true;
        }
}

    public void ShowWin()
    {
        Time.timeScale = 0;
        WINNER.SetActive(true);
        GameOverPanel.SetActive(false);
    }
    public void ShowDeath()
    {
        Time.timeScale = 0;
        WINNER.SetActive(false);
        GameOverPanel.SetActive(true);      
    }
    private void LateUpdate()
    {
        StartTimer();
    }
    void StartTimer(){
        if(StartedToPlay == true)
        {
            currentTime -= Time.deltaTime * 1;
            CountdownTimer.text = currentTime.ToString("0");
            if (Starting.EndsAnim == true)
            {
                currentTime = 0;
                StartingUI.SetActive(false);
            }
        }

    }
    public void StartToPlay()
    {
        StartedToPlay = true;
        Time.timeScale = 1;
        HowToPlayPanel.SetActive(false);
        Counter.SetActive(true);


    }
    /*
    IEnumerator TakeOutCakes()
    {
        while (true)
        {
           
            if (currentTime <= 0)
            {
                if(TimeToSpawn == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int piece = Random.Range(0, PieceCake.Count);
                        Rigidbody rig = PieceCake[piece].GetComponent<Rigidbody>();
                        PieceCake[piece].gameObject.tag = "PieceBye";
                        rig.isKinematic = false;
                        rig.velocity = Vector3.up * ElevateSpeed ;
                        Destroy(PieceCake[piece], 5);
                        PieceCake.Remove(PieceCake[piece]);

                    }
                    TimeTospawn();
                }

            }
                            yield return new WaitForSeconds(2f);

        }
    }
    */
    IEnumerator TakeOutCakes()
    {
        while (true)
        {

            if (currentTime <= 0)
            {
                if (TimeToSpawn == true)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int piece = Random.Range(0, PieceCake.Count);
                        PieceCake[piece].tag = "PieceBye";
                        FindObjectOfType<AudioManager>().Play("CakeUp");
                        Rigidbody rig = PieceCake[piece].GetComponent<Rigidbody>();
                        rig.isKinematic = false;
                        PieceCake.Remove(PieceCake[piece]);

                    }
                }

            }
            yield return new WaitForSeconds(2f);
            TimeTospawn();

        }
    }
    void TimeTospawn()
    {
        if (Random.value < 0.6)
        {
            TimeToSpawn = true;
        }
        else TimeToSpawn = false;
    }
    IEnumerator ForksHorde()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (currentTime <= 0)
            {
                foreach (Transform player in Players)
                {
                    if(player != null)
                    {
                        Vector3 spawPositionF = new Vector3(player.position.x, 2f, player.position.z);
                        Instantiate(Fork, spawPositionF, Quaternion.Euler(90, 0, 0));
                    }

                }

            
            }
            yield return new WaitForSeconds(Random.Range(3,4));

        }

    }

    int AngleToSpawn()
    {
        int angle = Random.Range(1, 9);
        //int angle = -45;
        switch (angle)
        {
            case 1:
                return 45;
            case 2:
                return -45;
            case 3:
                return 90;
            case 4:
                return 0;
            case 5:
                return 135;
            case 6:
                return -135;
            case 7:
                return 180;
            case 8:
                return -90;

            default:
                return -45;
        }




    }
    IEnumerator KnivesHorde()
    {

         while (true)
            {
            if (currentTime <= 0)
            {
                Vector3 SpawnLocation = new Vector3(Random.Range(PositionKnifes[0].x, PositionKnifes[0].y), 2f, Random.Range(PositionKnifes[1].x, PositionKnifes[1].y));
                Instantiate(Knife, SpawnLocation, Quaternion.Euler(0, AngleToSpawn(), 0));
               
            }
            yield return new WaitForSeconds(Random.Range(4,6));
        }
    }
    public void SceneChange(string nameScene)
    {
        StopAllCoroutines();
        SceneManager.LoadScene(nameScene);
    }
   
}

