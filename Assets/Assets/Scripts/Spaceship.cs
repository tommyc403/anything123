using System.Runtime.CompilerServices;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    public float EnginePower = 10f;
    public float TurnPower = 10f;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
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



    

 
}
