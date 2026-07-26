using JetBrains.Annotations;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public void SpawnNewAsteroid()
    {
       


        int asteroidIndex = Random.Range(0, AsteroidRefs.Length);
        GameObject asteroidRef = AsteroidRefs[asteroidIndex];
       

        Vector3 spawnPoint = OffscreenSpawnPoint();
        GameObject asteroid = Instantiate(asteroidRef, spawnPoint, Quaternion.identity);
        
  
        {
            Vector2 force = PushDirection(spawnPoint) * PushForce;
            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
          
            rb.AddForce(force);
        }
      

    }
    public GameObject[] AsteroidRefs;
    public float CheckInterval = 3f;
    public float PushForce = 0.5f;
    public int SpawnThreshold = 10;

    private float checkTimer = 0f;

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



    public float Inaccuracy = 2f;

    public Vector2 PushDirection(Vector2 from)
    {
        Vector2 miss = Random.insideUnitCircle * Inaccuracy;
        Vector2 destination = (Vector2)transform.position + miss;

        Vector2 direction = (destination - from).normalized;
        return direction;
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

   
    void Start()
   
        {
         
        }
  


    public void Update()
    {

       
        checkTimer += Time.deltaTime;
        if (checkTimer > CheckInterval)
        {
            checkTimer = 0f;

            int total = TotalAsteroidValue();


            if (total < SpawnThreshold)
            {
                SpawnNewAsteroid();
            }

            else {
            
            }
           
        }

    }

   

}
