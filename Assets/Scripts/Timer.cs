using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private const string GameKey = "GameTime";
    private const string BestTimeKey = "BestTime";

    public bool IsEnabled = true;
    public TMP_Text timerText;
    public TMP_Text fastestTimeText;
    public float timePassed = 0.0f;

    private void Awake()
    {
        if (fastestTimeText)
        {
            DisplayBestTime();
        }
        timePassed = 0.0f;
    }
    private void Start()
    {
        timePassed = PlayerPrefs.GetFloat(GameKey);
        UpdateTimer();
        IsEnabled = true;
    }

    private void Update()
    {
        if (IsEnabled)
        { 
            timePassed += Time.deltaTime;
            UpdateTimer();
            PlayerPrefs.SetFloat(GameKey, timePassed);
        }
    }

    public void DisplayBestTime()
    {
        if (PlayerPrefs.HasKey(BestTimeKey))
        {
            float fastestTime = PlayerPrefs.GetFloat(BestTimeKey);
            int minutes = Mathf.FloorToInt(fastestTime / 60F);
            int seconds = Mathf.FloorToInt(fastestTime % 60F);

            string timeString = string.Format("BEST TIME: {00:00}:{01:00}", minutes, seconds);
            fastestTimeText.text = timeString;
        }
        else
        {
            fastestTimeText.text = "Best Time: --:--";
        }
    }

    void UpdateTimer()
    {
        if (timerText)
        {
            int minutes = Mathf.FloorToInt(timePassed / 60f);
            int seconds = Mathf.FloorToInt(timePassed % 60f);
            string timeString = string.Format("TIME: {00:00}:{01:00}", minutes, seconds);
            timerText.text = timeString;
        }
    }

    public void StopTimer()
    {
        PlayerPrefs.SetFloat(GameKey, timePassed);
        if (PlayerPrefs.HasKey(BestTimeKey))
        {
            float fastestTime = PlayerPrefs.GetFloat(BestTimeKey);
            if (timePassed < fastestTime)
            {
                PlayerPrefs.SetFloat(BestTimeKey, timePassed);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(BestTimeKey, timePassed);
        }

        PlayerPrefs.SetFloat(GameKey, timePassed);
        UpdateTimer();

        IsEnabled = false;
    }

    //Les trois fonctions suivantes ont été prises de ChatGPT
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timePassed = PlayerPrefs.GetFloat(GameKey);
    }
    //
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(GameKey, 0.0f);
        //PlayerPrefs.DeleteKey(BestTimeKey);
    }
}
