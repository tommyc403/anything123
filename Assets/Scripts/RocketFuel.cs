using UnityEngine;

public class RocketFuel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ROCKET FUEL COLLISION WITH: " + collision.gameObject.name);

        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();

        if (spaceship != null)
        {
            Debug.Log("SPACESHIP FOUND!");

            spaceship.RefillFuel();

            Debug.Log("FUEL IS NOW: " + spaceship.FuelCurrent);

            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ROCKET FUEL TRIGGER WITH: " + collision.gameObject.name);
    }
}