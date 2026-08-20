using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFuel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {


        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();

        if (spaceship != null)
        {
            spaceship.RefillFuel();



            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}