using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject ExplosionRef;
    public float Damage = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            asteroid.TakeDamage(Damage);
            Instantiate(ExplosionRef, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
