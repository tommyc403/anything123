using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float MaxHealth = 3f;
    public float CurrentHealth;
    public float CollisionDamage = 1f;
    public int ScoreValue = 1;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship = collision.gameObject.GetComponent<Spaceship>();
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
    {
        Spaceship spacehsip = FindAnyObjectByType<Spaceship>();
    If(Spaceship != null)

    {
        Spaceship.AddScore(ScoreValue);
    }
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
