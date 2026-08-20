using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public int ScoreValue = 1;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship spaceship = collision.gameObject.GetComponent<Spaceship>();

        if (spaceship != null)
        {
            spaceship.CollectMinerals(ScoreValue);

            if (audioSource != null && audioSource.clip != null)
            {
                AudioSource.PlayClipAtPoint(
                    audioSource.clip,
                    transform.position
                );
            }

            Destroy(gameObject);
        }
    }
}