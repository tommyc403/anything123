using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


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
            ScoreTextBox.text = spaceShip.Score.ToString();
        }
    }

    

}
