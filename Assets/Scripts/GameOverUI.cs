using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text ScoreTextBox, HiScoreTextBox;
    public GameObject ScorePanel, Celebrate;
    private Spaceship spaceShip;
    private void Start()
    {
        spaceShip = FindObjectOfType<Spaceship>();
        Hide();
    }

    public void Show(bool celebrateHiScore)
    {
        ScoreTextBox.text = "SCORE " + spaceShip.Score.ToString();
        HiScoreTextBox.text = "HI SCORE " + spaceShip.GetHighScore().ToString();

        ScorePanel.SetActive(true);
    }

    public void Hide()
    {
        ScorePanel.SetActive(false);
    }

    public void ClickPlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Title");
    }
}
