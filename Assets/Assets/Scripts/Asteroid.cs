using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float MaxHealth = 3f;
    public float CurrentHealth;
    public float CollisionDamage = 1f;

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
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
