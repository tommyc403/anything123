using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] AsteroidRefs;
    public float CheckInterval = 3f;
    public float PushForce = 100f;
    public int SpawnThreshold = 10;
    public float Inaccuracy = 2f;

    private float checkTimer = 0f;



    private void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > CheckInterval)
        {
            checkTimer = 0f;
            if (TotalAsteroidValue() < SpawnThreshold)
            {
                SpawnNewAsteroid();
            }
        }
    }
    
    public Vector2 PushDirection(Vector2 from)
    {
        Vector2 miss = Random.insideUnitCircle * Inaccuracy;
        Vector2 destination = (Vector2)transform.position + miss;

        Vector2 direction = (destination - from).normalized;
        return direction;
    }

    private void SpawnNewAsteroid()
    {
        int asteroidIndex = Random.Range(0, AsteroidRefs.Length);
        GameObject asteroidRef = AsteroidRefs[asteroidIndex];

        Vector3 spawnPoint = OffscreenSpawnPoint();
        spawnPoint.z = transform.position.z; // remember to add this!

        GameObject asteroid = Instantiate(asteroidRef, spawnPoint, transform.rotation);

        Vector2 force = PushDirection(spawnPoint) * PushForce;

        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        rb.AddForce(force);
    }


    public Vector3 OffscreenSpawnPoint()
    {
        Vector2 randomPos = Random.insideUnitCircle;
        Vector3 direction = randomPos.normalized;
        Vector2 finalPos = transform.position + direction * 1f;

        
        return Camera.main.ViewportToWorldPoint(finalPos);
    }


    public int TotalAsteroidValue()
    {
        Asteroid[] asteroids = FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
        int value = 0;
        for(int n = 0; n < asteroids.Length; n++)
        {
            value += asteroids[n].SpawnValue;
        }
        return value;
    }

}
