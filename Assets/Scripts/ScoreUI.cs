using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TMP_Text ScoreTextBox;

    private Spaceship spaceShip;

    private void Start()
    {
        spaceShip = FindObjectOfType<Spaceship>();
    }

    private void Update()
    {
        if (spaceShip != null)
        {
            ScoreTextBox.text = "SCORE: " + spaceShip.MineralsCollected;
        }
    }
}