using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Slider FuelSlider;

    private Spaceship spaceShip;

    private void Start()
    {
        spaceShip = FindObjectOfType<Spaceship>();

        if (spaceShip == null)
        {
            Debug.LogError("FuelUI could not find a Spaceship!");
            return;
        }

        if (FuelSlider != null)
        {
            FuelSlider.minValue = 0f;
            FuelSlider.maxValue = spaceShip.FuelMax;
            FuelSlider.value = spaceShip.FuelCurrent;
        }
    }

    private void Update()
    {
        if (spaceShip != null && FuelSlider != null)
        {
            FuelSlider.value = spaceShip.FuelCurrent;
        }
    }
}