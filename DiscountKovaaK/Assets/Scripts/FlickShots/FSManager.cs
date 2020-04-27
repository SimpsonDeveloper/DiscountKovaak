using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FSManager : MonoBehaviour
{
    public Vector3 bottomLeft;
    public Vector2 bounds;
    public GameObject button;
    public GameObject target;
    public Text timerText;
    public Text scoreText;
    public Text introText;
    public Text previousScore;
    private GameObject tile;
    private float timeLeft;
    private const float timeLimit = 30.0f;
    private int score;
    private bool sessionStarted;
    private static FSManager _instance = null;
    public static FSManager Instance
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
        }
    }

    void Reset()
    {
        timeLeft = timeLimit;
        sessionStarted = false;
        //clear tiles
        if (tile != null)
            Destroy(tile);
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
            ResetScore();
        }
         //spawn a tile
        Vector3 spawnPoint = bottomLeft + new Vector3(Random.Range(0.0f, 1.0f) * bounds.x, Random.Range(0.0f, 1.0f) * bounds.y, 0.0f);
        tile = Object.Instantiate(target, spawnPoint, new Quaternion());
        button.SetActive(false);
    }

    public void TargetClicked(GameObject target)
    {
        button.SetActive(true);
        IncrementScore();
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
