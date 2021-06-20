using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    private int x;
    private int y;
    private int ReachedGoal = 0;
    private int HitEndCollider = 0;

    private int ScoreCount;
    public bool PlayerDestroyed;

    public GameObject MenuManager;
    public GameObject MapManager;
    public GameObject MovementManager;
    private GameObject[] Fragments;

    public GameObject PBtn0;
    public GameObject PBtn1;
    public GameObject ShieldIcon;
    public Sprite ShieldSprite;
    public Sprite DefaultButton;

    public Camera cam;
    public Color color2;

    public Text FinalScore;
    private int HighScore;
    public Text HighScoreText;
    public Text Score;
    private string[] Powerups = {null, null};
    private bool Shielded;
    private Rigidbody2D RigidBody;

    void Start()
    {
        Application.targetFrameRate = 120;
        RigidBody = GetComponent<Rigidbody2D>();
        Powerups[0] = "none";
        Powerups[1] = "none";
        ShieldIcon.SetActive(false);
        try
        {
            HighScore = PlayerPrefs.GetInt("HighScore");
        }
        catch
        {
            HighScore = 0;
        }
        HighScoreText.text = HighScore.ToString();
    }

    void Update()
    {
        if(ReachedGoal == 1)
        {
           RigidBody.AddForce(new Vector2(0, 1.7f), ForceMode2D.Impulse);
        }

        if(cam.backgroundColor != color2)
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, color2, Time.deltaTime * 0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if(!Shielded)
            {
                StartCoroutine(DestroyPlayer());
            }
        }

        if(other.tag == "BeforeStart")
        {
            other.gameObject.transform.parent.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if(other.tag == "Shield")
        {
            if(other.gameObject.transform.parent.gameObject != null)
            {
                Destroy(other.gameObject.transform.parent.gameObject);
                AddPowerup("Shield");
            }
        }

        if(other.tag == "Exit"){
            HitEndCollider ++;
            ReachedEnd();
        }

        if(other.tag == "Start")
        {
            ReachedGoal = 0;
            HitEndCollider = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            other.gameObject.transform.parent.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (other.tag == "EnemyGate")
        {
            other.gameObject.transform.Find("EnemyGate").GetComponent<Collider2D>().isTrigger = true;
        }
    }

    public void AddPowerup(string powerup)
    {
        if(Powerups[0] == "none")
        {
            Powerups[0] = powerup;
            PBtn0.GetComponent<Image>().sprite = ShieldSprite;
            
        }
        else if(Powerups[1] == "none")
        {
            Powerups[1] = powerup;
            PBtn0.GetComponent<Image>().sprite = ShieldSprite;
        }
    }

    public void PowerUpPressed(int button)
    {
        StartCoroutine(ActivatePowerup(Powerups[button], button));
    }

    IEnumerator ActivatePowerup(string powerup, int button)
    {
        Debug.Log(button);
        Debug.Log(Powerups[button]);
        if(Powerups[button] == "Shield")
        {
            if(!Shielded)
            {
                PBtn0.GetComponent<Image>().sprite = DefaultButton;
                Powerups[button] = "none";
                ShieldIcon.SetActive(true);
                Shielded = true;

                yield return new WaitForSeconds(5);

                ShieldIcon.SetActive(false);
                Shielded = false;
            }
        }
        yield return null;
    }

    IEnumerator DestroyPlayer()
    {
        yield return new WaitForSeconds(0.2f); //wait
        
        GetComponent<Explodable>().explode();
        Fragments = GameObject.FindGameObjectsWithTag("FragmentP");

        for(int i = 0; i < Fragments.Length; i++)
        {
            x = Random.Range(0, 30);
            y = Random.Range(0, 30);
            Fragments[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
        }
        
        MenuManager.GetComponent<MenuControls>().GameOver();
        FinalScore.text = ScoreCount.ToString();
        if(HighScore < ScoreCount)
        {
            PlayerPrefs.SetInt("HighScore", ScoreCount);
            HighScoreText.text = ScoreCount.ToString();
        }
        else
        {
            HighScoreText.text = HighScore.ToString();
        }

    }

    void ReachedEnd()
    {
        if(HitEndCollider == 1)
        {
            color2 = UnityEngine.Random.ColorHSV();
            MovementManager.GetComponent<MovePlayer>().RefreshEnemiesArray();
            // MapManager.GetComponent<MapManager>().ActivateTile();
            ReachedGoal = 1;
            ScoreCount += 1;
            Score.text = ScoreCount.ToString();
            MapManager.GetComponent<MapManager>().MoveCamera();
            MapManager.GetComponent<MapManager>().SpawnNewTile();
        }
    }
}