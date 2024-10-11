using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    TMP_Text timerText;
    float timePassed = 0.0f;

    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
        // Load the timer from PlayerPrefs when starting the game
        if (PlayerPrefs.HasKey("GameTime"))
        {
            timePassed = PlayerPrefs.GetFloat("GameTime");
        }
        else
        {
            timePassed = 0.0f;
        }

        UpdateTimer();
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        UpdateTimer();
        PlayerPrefs.SetFloat("GameTime", timePassed);
    }

    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timePassed / 60f);
        int seconds = Mathf.FloorToInt(timePassed % 60f);
        string timeString = string.Format("{00:00}:{01:00}", minutes, seconds);
        timerText.text = timeString;
    }

    public void StopTimer()
    {
        PlayerPrefs.SetFloat("FinalGameTime", timePassed);  
        PlayerPrefs.Save();
        enabled = false;  // Disable this script so it stops updating
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // The timer keeps running even after a new scene is loaded
        if (PlayerPrefs.HasKey("GameTime"))
        {
            timePassed = PlayerPrefs.GetFloat("GameTime");
        }
    }
}
