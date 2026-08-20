using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Slider FuelSlider;

    private Spaceship spaceShip;
    // Start is called before the first frame update
    private void Start()
    {
        spaceShip = FindObjectOfType<Spaceship>();

        if (spaceShip == null)
        {
   
            return;
        }
        //FUEL SLIDER Connect
        if (FuelSlider != null)
        {
            FuelSlider.minValue = 0f;
            FuelSlider.maxValue = spaceShip.FuelMax;
            FuelSlider.value = spaceShip.FuelCurrent;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //Link Fuel Slider to fuel current
        if (spaceShip != null && FuelSlider != null)
        {
            FuelSlider.value = spaceShip.FuelCurrent;
        }
    }
}