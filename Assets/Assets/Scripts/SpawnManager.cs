using JetBrains.Annotations;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int TotalAsteroidValue()
    {
        Asteroid[] asteroids =
            FindObjectsByType<Asteroid>(FindObjectsSortMode.None);
        int value = 0;
        for (int n = 0; n < asteroids.Length; n++)
        {
            value += asteroids[n].SpawnValue;
        }
        return value;
    }

    public GameObject[] AsteroidRefs;
    public float CheckInterval = 3f;
    public float Pushforce = 100f;
    public int SpawnThreshold = 10;

    public float checkTimer = 0f;
    public float Inaccuracy = 2f;

    public Vector2 PushDirection(Vector2 from)
    {
        Vector2 miss = Random.insideUnitCircle * Inaccuracy;
        Vector2 destination = (Vector2)transform.position + miss;

        Vector2 direction = (destination - from).normalized;
        return direction;
    }

    public void SpawnNewAsteroid()
    {
        Vector2 force = PushDirection(OffscreenSpawnPoint) * Pushforce;
        Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
        rb.AddForce(force);

    }

    public Vector3 OffscreenSpawnPoint()
    {
        Vector2 randomPos = Random.insideUnitCircle;
        Vector2 direction = randomPos.normalized;
        Vector2 finalPos = (Vector2)transform.position + direction * 2f;
        Vector3 result = Camera.main.ViewportToWorldPoint(finalPos);
        result.z = transform.position.z;

        return result;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > CheckInterval)
        {
            checkTimer = 0f;

            if (TotalAsteroidValue() < SpawnThreshold)
            {
                SpawnNewAsteroid();
                {
                    int asteroidIndex = Random.Range(0, AsteroidRefs.Length);
                    GameObject asteroidRef = AsteroidRefs[asteroidIndex];

                    Vector3 spawnPoint = OffscreenSpawnPoint();

                    GameObject asteroid = Instantiate(asteroidRef, spawnPoint, transform.rotation);
                }
            }
        }

    }
    
}
