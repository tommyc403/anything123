using UnityEngine;
using TMPro;


public class ScoreUI : MonoBehaviour
{
    public TMP_Text HullIntegrityText;
    private Spaceship ship;

    void Start()
    {
        Debug.Log("Scoreboard thing testing");

        ship = Object.FindFirstObjectByType<Spaceship>();

        if (ship == null)
        {
           
        }
        else
        {
           
        }

        if (HullIntegrityText == null)
        {
            
        }
        else
        {
            
            HullIntegrityText.text = "TESTES";
        }
        HullIntegrityText.text = "Dammit please work";
        HullIntegrityText.color = Color.orangeRed;
        HullIntegrityText.fontSize = 72;
    }

    void Update()
    {
        if (ship != null)
        {
            HullIntegrityText.text = "HULL INTEGRITY: " + Mathf.CeilToInt(ship.CurrentHealth);
        }
       
    }
}
