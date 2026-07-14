using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float HealthMax = 3f;

    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = HealthMax; 
    }
   
    public float CollisionDamage = 1f;
    
    public int SpawnValue = 3;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship =
        collision.gameObject.GetComponent<Spaceship>();
        if (ship != null)
        {
            ship.TakeDamage(CollisionDamage);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
        if (CurrentHealth <= 0) 
        {
            Explode();
        }

            
    }

    public void Explode()
    {
        Debug.Log("Asteroid Obliterated");
        Destroy(gameObject);
    }

    void Start()
    {
        CurrentHealth = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
