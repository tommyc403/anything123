using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public GameOverUI GameOverUI;
    public ScreenFlash ScreenFlash;
    public float EnginePower = 10f;
    public float EngineReversePower = 10f;
    public float TurnPower = 10f;
    public float HealthMax = 3f;

    public float FiringRate = 0.66f;
    private float firingTimer = 0f;

    public GameObject BulletRef;
    public float BulletSpeed = 100f;

    public GameObject ExplosionRef;
    public int Score = 0;


    private float healthCurrent;


    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        healthCurrent = HealthMax;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFiring();

        if (Input.GetKey(KeyCode.A))
        {
            ApplyTorque(-1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyTorque(1f);
        }

        float vert = Input.GetAxis("Vertical");

        if (vert > 0f)
        {
            ApplyThrust(vert);
        }
        else if (vert < 0f)
        {
            ApplyReverseThrust(-vert);
        }
    }


    private void UpdateFiring()
    {
        bool isfiring = Input.GetButton("Fire1");
        firingTimer = firingTimer - Time.deltaTime;

        if (isfiring && firingTimer <= 0f)     
        {
            FireBullet();
            firingTimer = FiringRate;
        }
    }
    public void ApplyThrust(float amount)
   {
        Vector2 thrust = transform.up * EnginePower * Time.deltaTime * amount;
        rigidBody.AddForce(thrust);
    }

    public void ApplyReverseThrust(float amount)
    {
        Vector2 thrust = -transform.up * EngineReversePower * Time.deltaTime * amount;
        rigidBody.AddForce(thrust);
    }

    public void ApplyTorque(float amount)
    {
        float torque = amount * TurnPower * Time.deltaTime;
        rigidBody.AddTorque(torque);
    }

    public void TakeDamage(float damage)
    {
        // Only flash the screen if actual damage is being dealt
        if (damage > 0f)
        {
            if (ScreenFlash != null)
            {
                ScreenFlash.DoScreenFlash();
            }
        }

        // Reduce health
        healthCurrent -= damage;

        // If no health remains, explode
        if (healthCurrent <= 0f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // ends the game
        Instantiate(ExplosionRef, transform.position, transform.rotation);

        GameOver();
        Destroy(gameObject);
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(BulletRef,
                                        transform.position,
                                        transform.rotation);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 force = BulletSpeed * transform.up;

        rb.AddForce(force);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("Highscore", score);
    }

    public void GameOver()
    {
        // TODO add some delay to give a chance to see the player explosion.


        bool celebrateHiScore = false;
        if (Score > GetHighScore())
        {
            SetHighScore(Score);
            celebrateHiScore = true;
        }
        // show gameover UI
        this.GameOverUI.Show(celebrateHiScore);

        // other code here
    }


}
