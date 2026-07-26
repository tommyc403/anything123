using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public GameObject[] Chunks;
    public GameObject ExplosionRef;
    public GameObject[] ChunkRefs;
    public int ChunksMin = 0;
    public int ChunksMax = 4;

    public float ExplodeDist = 0.5f;
    public float ExplosionForce = 10f;

    public float HealthMax = 3f;
    public float CurrentHealth;
    public float Damage = 1f;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            asteroid.TakeDamage(Damage);
            Explode();
            Destroy(gameObject);
        }
    }


    public void TakeDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
        if (CurrentHealth <= 0)
        {
            Explode();
            {
                Debug.Log("Asteroid Obliterated");
                Destroy(gameObject);
            }
            Destroy(gameObject);
            {
                CurrentHealth = HealthMax;
            }
        }


    }
    private void Explode()
    {

        int n = 0;
        Instantiate(Chunks[n], transform.position, transform.rotation);

        int numChunks = Random.Range(ChunksMin, ChunksMax);

        for (int i = 0; i < numChunks; i++)
        {
            CreateAsteroidChunk();
            Destroy(gameObject);

        }


        Destroy(gameObject);
        Instantiate(ExplosionRef, transform.position, transform.rotation);
        Destroy(gameObject);

    }
    private void CreateAsteroidChunk()
    {
        int randomIndex = Random.Range(0, ChunkRefs.Length);
        GameObject chunkRef = ChunkRefs[randomIndex];

        Vector2 spawnPos = transform.position;
        spawnPos.x += Random.Range(-ExplodeDist, ExplodeDist);
        spawnPos.y += Random.Range(-ExplodeDist, ExplodeDist);

        GameObject chunk = Instantiate(chunkRef, spawnPos, transform.rotation);

        Vector2 dir = (spawnPos = transform.position).normalized;

        Rigidbody2D rb = chunk.GetComponent<Rigidbody2D>();
        rb.AddForce(dir * ExplosiveForce);
        Destroy(gameObject);
    }

   
    public int SpawnValue = 3;
 

    private void Start()
    {
        CurrentHealth = HealthMax;
    }

    public float CollisionDamage = 1f;

    public Vector2 ExplosiveForce { get; private set; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship =
        collision.gameObject.GetComponent<Spaceship>();
        if (ship != null)
        {
            ship.TakeDamage(CollisionDamage);
        }
    }

}
