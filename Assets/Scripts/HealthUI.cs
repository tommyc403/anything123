using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public TMP_Text HealthTextBox;

    private Spaceship spaceShip;

    private void Start()
    {
        spaceShip = FindObjectOfType<Spaceship>();
    }

    private void Update()
    {
        if (spaceShip != null)
        {
            HealthTextBox.text = "HEALTH: " +
                                 spaceShip.HealthCurrent.ToString("0") +
                                 " / " +
                                 spaceShip.HealthMax.ToString("0");
        }
    }
}