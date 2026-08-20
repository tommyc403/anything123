using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorativeOre : MonoBehaviour
{
    public float HealAmount = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();

        if (spaceship != null)
        {
            Debug.Log("RESTORATIVE ORE COLLECTED - HEALING " + HealAmount);

            spaceship.RestoreHealth(HealAmount);

            Destroy(gameObject);
        }
    }
}
