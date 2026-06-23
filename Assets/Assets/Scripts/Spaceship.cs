using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    public float EnginePower = 10f;
    public float TurnPower = 10f;
    public float MaxHealth = 3f;
    public float CurrentHealth;


    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        ApplyThrust(vert);
        ApplyTorque(horiz);
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
    public void TakeDamage(float damage)
    { 

     CurrentHealth = CurrentHealth - damage;
        //CurrentHealth -= damage;
        if (CurrentHealth <= 0)
    
        Explode();
    }

    public void Explode()
    {
        Debug.Log("GAME OVER");
        Destroy(gameObject);
    }
}







    

 

