using UnityEngine;

public class Asteroid : MonoBehaviour
{
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
