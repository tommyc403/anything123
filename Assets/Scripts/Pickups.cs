using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
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
            // Spaceship collision detection:
            ship.TakeDamage(CollisionDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        healthCurrent = healthCurrent - damage;
        if (healthCurrent <= 0f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // modify the score
        Spaceship spaceship = GetComponent<Spaceship>();
        if (spaceship != null)
        {
            spaceship.Score += ScoreValue;
        }


        int numChunks = Random.Range(ChunksMin, ChunksMax + 1);
        for(int i = 0; i < numChunks; i++)
        {
            CreateAsteroidChunk();
        }
        
        if (ExplosionRef)
        {
            Instantiate(ExplosionRef, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void CreateAsteroidChunk()
    {
        if (ChunkRefs == null || ChunkRefs.Length == 0)
        {
            return;
        }


        // find prefab to instantiate
        int randomIndex = Random.Range(0, ChunkRefs.Length);
        GameObject chunkRef = ChunkRefs[randomIndex];

        Vector2 myPos = transform.position;
        // find a random pos to spawn at
        Vector2 spawnPos = transform.position;
        spawnPos.x += Random.Range(-ExplodeDist, ExplodeDist);
        spawnPos.y += Random.Range(-ExplodeDist, ExplodeDist);

        // instantiate the asteroid
        GameObject chunk = Instantiate(chunkRef, spawnPos, transform.rotation);

        // find dir to chunk
        Vector2 dir = (spawnPos - myPos).normalized;

        // apply force
        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * ExplosionForce);


    }

}
