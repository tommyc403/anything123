using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int ScoreValue = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();

        if (spaceship != null)
        {
            spaceship.CollectMinerals(ScoreValue);

            Destroy(gameObject);
        }
    }
}