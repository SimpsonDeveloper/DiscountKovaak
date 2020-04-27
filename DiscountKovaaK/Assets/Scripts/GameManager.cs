using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float xSensitivity = 70.0f;
    public float ySensitivity = 70.0f;
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadHub()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadFlickShots()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadTileFrenzy()
    {
        SceneManager.LoadScene(2);
    }

    public void SetXSensitivity(float value)
    {
        xSensitivity = value * Conversions.SliderModifier;
    }

    public void SetYSensitivity(float value)
    {
        ySensitivity = value * Conversions.SliderModifier;
    }
}
