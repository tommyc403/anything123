using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroids : MonoBehaviour
{
    public int ScoreValue = 10;
    public int SpawnValue = 1;

    public float CollisionDamage = 1f;

    public int ChunksMin = 0;
    public int ChunksMax = 4;
    public float ExplodeDist = 0.5f;
    public float ExplosionForce = 10f;

    // New chunk things- Serialize lets objects with this script break into these things
    [SerializeField] private GameObject GoldChunk;
    [SerializeField] private GameObject ValuableChunk;
    [SerializeField] private GameObject RocketFuelChunk;
    [SerializeField] private GameObject RestorativeOreChunk;

    public float HealthMax = 5f;
    private float healthCurrent;

    public GameObject ExplosionRef;

    private void Start()
    {
        healthCurrent = HealthMax;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship = collision.gameObject.GetComponent<Spaceship>();

        if (ship != null)
        {
            // We've collided with a spaceship!
            ship.TakeDamage(CollisionDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        healthCurrent -= damage;

        if (healthCurrent <= 0f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // Modify the score
        Spaceship spaceship = GetComponent<Spaceship>();

        if (spaceship != null)
        {
            spaceship.Score += ScoreValue;
        }

        // Decide how many chunks to create
        int numChunks = Random.Range(ChunksMin, ChunksMax + 1);

        for (int i = 0; i < numChunks; i++)
        {
            CreateAsteroidChunk();
        }

        // Create explosion effect
        if (ExplosionRef)
        {
            Instantiate(ExplosionRef, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void CreateAsteroidChunk()
    {
        // roll d100
        int roll = Random.Range(0, 100);

        GameObject chunkRef;

        if (roll < 90)
        {
            // common roll GOLD CHUNKS
            chunkRef = GoldChunk;
        }
        else if (roll < 95)
        {
            // unlikely roll VALUABLE CHUNKS
            chunkRef = ValuableChunk;
        }
        else if (roll < 98)
        {
            // rare roll ROCKET FUEL CHUNKS
            chunkRef = RocketFuelChunk;
        }
        else
        {
            // very rare roll RESTORATIVE ORE CHUNKS
            chunkRef = RestorativeOreChunk;
        }

        // Make sure a prefab has actually been assigned
        if (chunkRef == null)
        {
            Debug.LogWarning("A chunk prefab has not been assigned to " + gameObject.name);
            return;
        }

        Vector2 myPos = transform.position;

        // Find a random position to spawn the chunk
        Vector2 spawnPos = transform.position;

        spawnPos.x += Random.Range(-ExplodeDist, ExplodeDist);
        spawnPos.y += Random.Range(-ExplodeDist, ExplodeDist);

        // Instantiate the asteroid chunk
        GameObject chunk = Instantiate(chunkRef, spawnPos, transform.rotation);

        // Find direction from the asteroid to the chunk
        Vector2 dir = (spawnPos - myPos).normalized;

        // Apply force
        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(dir * ExplosionForce);
        }
    }
}