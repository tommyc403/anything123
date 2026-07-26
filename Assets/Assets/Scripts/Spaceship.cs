using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public float FiringRate = 0.33f;

    private float fireTimer = 0f;

    public float EnginePower = 10f;
    public float TurnPower = 10f;
    public float MaxHealth = 3f;
    public float CurrentHealth;

    public GameObject BulletRef;
    public float BulletSpeed = 100f;
    public void TakeDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
        //CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Debug.Log("Your piloting skills need some work...");
        Destroy(gameObject);
    }

    public void FireBullet()
    {

        GameObject bullet = Instantiate(BulletRef, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 force = transform.up * BulletSpeed;

        rb.AddForce(force);
    }


    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
    }





    public void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(BulletRef, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 force = transform.up * BulletSpeed;

            rb.AddForce(force);
        }
    }

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");
        fireTimer = fireTimer - Time.deltaTime;
        if (isFiring && fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = FiringRate;
        }
    }
    private void ApplyThrust(float amount)
    {
        Vector2 thrust = transform.up * EnginePower * Time.deltaTime * amount;
        rb2D.AddForce(thrust);
    }
    private void ApplyTorque(float amount)
    {
        float torque = amount * TurnPower * Time.deltaTime;
        rb2D.AddTorque(torque);


    }
    
}








    

 

