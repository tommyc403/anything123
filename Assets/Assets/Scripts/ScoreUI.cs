using UnityEngine;
using TMPro;


public class ScoreUI : MonoBehaviour
{
    public TMP_Text HullIntegrityText;
    private Spaceship ship;

    void Start()
    {
        ship = Object.FindFirstObjectByType<Spaceship>();
        HullIntegrityText.text = "TEST";
    }

    void Update()
    {
        if (ship != null)
        {
            HullIntegrityText.text = "HULL INTEGRITY: " + Mathf.CeilToInt(ship.CurrentHealth);
        }
    }
}
