using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int ScoreValue = 10;
    public int SpawnValue = 1;
    public GameObject[] ChunkRefs;
    public float CollisionDamage = 1f;

    public int ChunksMin = 0;
    public int ChunksMax = 4;
    public float ExplodeDist = 0.5f;
    public float ExplosionForce = 10f;

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
            ship.TakeDamage(CollisionDamage);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        healthCurrent -= damage;

        if (healthCurrent <= 0f)
        {
            Explode();
        }
    }

    public virtual void Explode()
    {
        Spaceship spaceship = GetComponent<Spaceship>();

        if (spaceship != null)
        {
            spaceship.Score += ScoreValue;
        }

        int numChunks = Random.Range(ChunksMin, ChunksMax + 1);

        for (int i = 0; i < numChunks; i++)
        {
            CreateAsteroidChunk();
        }

        if (ExplosionRef)
        {
            Instantiate(ExplosionRef, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected virtual void CreateAsteroidChunk()
    {
        if (ChunkRefs == null || ChunkRefs.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, ChunkRefs.Length);
        GameObject chunkRef = ChunkRefs[randomIndex];

        SpawnChunk(chunkRef);
    }

    protected void SpawnChunk(GameObject chunkRef)
    {
        Vector2 myPos = transform.position;

        Vector2 spawnPos = transform.position;

        spawnPos.x += Random.Range(-ExplodeDist, ExplodeDist);
        spawnPos.y += Random.Range(-ExplodeDist, ExplodeDist);

        GameObject chunk = Instantiate(chunkRef, spawnPos, transform.rotation);

        Vector2 dir = (spawnPos - myPos).normalized;

        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.AddForce(dir * ExplosionForce);
        }
    }
}