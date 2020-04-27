using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TFManager : MonoBehaviour
{
    public Vector3 bottomLeft;
    public Vector2 bounds;
    public GameObject button;
    public GameObject target;
    public Text timerText;
    public Text scoreText;
    public Text introText;
    public Text previousScore;
    public int maxTiles;
    public float spawnInterval;
    private List<GameObject> tiles = new List<GameObject>();
    private float timeLeft;
    private const float timeLimit = 60.0f;
    private float spawnAtTime;
    private int score;
    private bool sessionStarted;
    private static TFManager _instance = null;
    public static TFManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
        score = 0;
        Reset();
    }

    void Update()
    {
        if (sessionStarted)
        {
            //decrement time
            timeLeft -= Time.deltaTime;
            timerText.text = "Time Left: " + timeLeft.ToString("0.00");
            if (timeLeft < 0)
                Reset();
            else
            {
                while (timeLeft <= spawnAtTime && tiles.Count < maxTiles)
                {
                    //spawn a tile
                    Vector3 spawnPoint = bottomLeft + new Vector3(Random.Range(0.0f, 1.0f) * bounds.x, Random.Range(0.0f, 1.0f) * bounds.y, 0.0f);
                    tiles.Add(Object.Instantiate(target, spawnPoint, new Quaternion()));
                    //decrement spawnAtTime
                    spawnAtTime -= spawnInterval;
                }
            }

            
        }
    }

    void Reset()
    {
        timeLeft = timeLimit;
        spawnAtTime = timeLimit;
        sessionStarted = false;
        //clear tiles
        if (tiles != null)
            for (int i = 0; i < tiles.Count; i++)
            {
                Destroy(tiles[i]);
            }
        //reset button
        button.SetActive(true);
        //clear the HUD text
        timerText.text = "";
        scoreText.text = "";
        //show the intro text
        introText.enabled = true;
        previousScore.text = "Previous Score: " + score.ToString();
        previousScore.enabled = true;

    }

    public void ButtonClicked()
    {
        if (!sessionStarted)
        {
            //clear the intro text
            introText.enabled = false;
            previousScore.enabled = false;
            //start the game
            sessionStarted = true;
            button.SetActive(false);
            ResetScore();
        }
    }

    public void TargetClicked(GameObject target)
    {
        IncrementScore();
        tiles.Remove(target);
        Destroy(target);
    }

    void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
}
