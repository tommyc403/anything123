using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject ExplosionRef;
    public float Damage = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid)
        {
            asteroid.TakeDamage(Damage);
            Explode();
        }
    }
    private void Explode()
    {
        Instantiate(ExplosionRef, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
