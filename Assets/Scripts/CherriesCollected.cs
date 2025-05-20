using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CherriesCollected : MonoBehaviour
{
    TMP_Text cherriesCollectedText;
    private int totalCherriesCollected = 0;

    private void Start()
    {
        cherriesCollectedText = GetComponent<TMP_Text>();
        UpdateCherryText();
    }

    void UpdateCherryText()
    {
        cherriesCollectedText.text = "Cherries: " + totalCherriesCollected;
    }

    internal void CollectCherry()
    {
        totalCherriesCollected++;
        UpdateCherryText();
    }

    public void OnLevelChange()
    {
        totalCherriesCollected = 0;  // Reset the counter when changing levels
        UpdateCherryText();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnLevelChange();  // Reset fruit count when a new level is loaded
    }
}
