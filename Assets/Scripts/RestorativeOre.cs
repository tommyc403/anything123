using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorativeOre : MonoBehaviour
{
    public float HealAmount = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();
        //Simple heals [NEW MAX 3]
        if (spaceship != null)
        {

            spaceship.RestoreHealth(HealAmount);

            Destroy(gameObject);
        }
    }
}
