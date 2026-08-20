using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public GameOverUI GameOverUI;
    public ScreenFlash ScreenFlash;
    public GameObject BulletRef;
    public GameObject ExplosionRef;
    private Rigidbody2D rigidBody;


// Basic Movement

    public float EnginePower = 10f;
    public float EngineReversePower = 10f;
    public float TurnPower = 10f;


//Booster

    public float BoostMultiplier = 3f;

    public float FuelMax = 100f;
    private float fuelCurrent;

    public float FuelCurrent => fuelCurrent;

    // Amount of fuel consumed while boosting
    public float FuelDrainAmount = 20f;

    // How often fuel is consumed while boosting
    public float FuelDrainInterval = 1f;

    private float fuelDrainTimer = 0f;

    // Amount of fuel regenerated
    public float FuelRechargeAmount = 20f;

    // How often fuel regenerates
    public float FuelRechargeInterval = 20f;

    private float fuelRechargeTimer = 0f;


    // =========================================================
    // FIRING
    // =========================================================

    public float FiringRate = 0.66f;
    private float firingTimer = 0f;

    public float BulletSpeed = 100f;


    // =========================================================
    // HEALTH
    // =========================================================

    public float HealthMax = 3f;
    private float healthCurrent;

    public float HealthCurrent => healthCurrent;


    // =========================================================
    // SCORE / MINERALS
    // =========================================================

    public int MineralsCollected = 0;


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        healthCurrent = HealthMax;
        fuelCurrent = FuelMax;

        // Begin the fuel recharge countdown.
        fuelRechargeTimer = FuelRechargeInterval;
    }


    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        UpdateMovement();
        UpdateFiring();
        UpdateFuelDrain();
        UpdateFuelRecharge();
    }


    // =========================================================
    // MOVEMENT
    // =========================================================

    private void UpdateMovement()
    {
        // -----------------------------------------
        // Rotation
        // -----------------------------------------

        if (Input.GetKey(KeyCode.A))
        {
            ApplyTorque(-1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyTorque(1f);
        }


        // -----------------------------------------
        // Forward / Reverse Thrust
        // -----------------------------------------

        float vert = Input.GetAxis("Vertical");

        if (vert > 0f)
        {
            float thrustMultiplier = 1f;

            // Activate boost while:
            // 1. Space is held
            // 2. Fuel remains
            if (Input.GetKey(KeyCode.Space) && fuelCurrent > 0f)
            {
                thrustMultiplier = BoostMultiplier;
            }

            ApplyThrust(vert * thrustMultiplier);
        }
        else if (vert < 0f)
        {
            ApplyReverseThrust(-vert);
        }
    }


    public void ApplyThrust(float amount)
    {
        Vector2 thrust =
            transform.up *
            EnginePower *
            Time.deltaTime *
            amount;

        rigidBody.AddForce(thrust);
    }


    public void ApplyReverseThrust(float amount)
    {
        Vector2 thrust =
            -transform.up *
            EngineReversePower *
            Time.deltaTime *
            amount;

        rigidBody.AddForce(thrust);
    }


    public void ApplyTorque(float amount)
    {
        float torque =
            amount *
            TurnPower *
            Time.deltaTime;

        rigidBody.AddTorque(torque);
    }


    // =========================================================
    // FUEL
    // =========================================================

    public void AddFuel(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        fuelCurrent += amount;

        if (fuelCurrent > FuelMax)
        {
            fuelCurrent = FuelMax;
        }
    }


    public void UseFuel(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        fuelCurrent -= amount;

        if (fuelCurrent < 0f)
        {
            fuelCurrent = 0f;
        }
    }


    public void RefillFuel()
    {
        fuelCurrent = FuelMax;
    }


// Fuel draining when accelerating

    private void UpdateFuelDrain()
    {
        float vert = Input.GetAxis("Vertical");

        bool isBoosting =
            Input.GetKey(KeyCode.Space) &&
            vert > 0f &&
            fuelCurrent > 0f;

        if (isBoosting)
        {
            fuelDrainTimer -= Time.deltaTime;

            if (fuelDrainTimer <= 0f)
            {
                UseFuel(FuelDrainAmount);

                fuelDrainTimer = FuelDrainInterval;
            }
        }
        else
        {
            fuelDrainTimer = 0f;
        }
    }


  // Fuel regeneration

    private void UpdateFuelRecharge()
    {
       //Cap at maximum
        if (fuelCurrent >= FuelMax)
        {
            fuelRechargeTimer = FuelRechargeInterval;
            return;
        }

        fuelRechargeTimer -= Time.deltaTime;

        if (fuelRechargeTimer <= 0f)
        {
            AddFuel(FuelRechargeAmount);

            fuelRechargeTimer = FuelRechargeInterval;
        }
    }


// OH SHOOT

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");

        firingTimer -= Time.deltaTime;

        if (isFiring && firingTimer <= 0f)
        {
            FireBullet();
            //Link to new held firing rate
            firingTimer = FiringRate;
        }
    }


    public void FireBullet()
    {
        GameObject bullet = Instantiate(
            BulletRef,
            transform.position,
            transform.rotation
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 force = BulletSpeed * transform.up;

        rb.AddForce(force);
    }


// Health and Damage

    public void TakeDamage(float damage)
    {

        if (damage <= 0f)
        {
            return;
        }

        if (ScreenFlash != null)
        {
            ScreenFlash.DoScreenFlash();
    }

        healthCurrent -= damage;

        if (healthCurrent <= 0f)
        {
            Explode();
        }
    }


    public void RestoreHealth(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        healthCurrent += amount;

        if (healthCurrent > HealthMax)
        {
            healthCurrent = HealthMax;
        }
    }


// MINERAL SCOREBOARD
    public void CollectMinerals(int amount)
    {
        MineralsCollected += amount;
    }


// ANDREW'S GAME OVER SCREEN **LEAVE**

    public void Explode()
    {
        Instantiate(
            ExplosionRef,
            transform.position,
            transform.rotation
        );

        GameOver();

        Destroy(gameObject);
    }

    public void GameOver()
    {
        bool celebrateHiScore = false;
        //Change to Minerals instead of Score
        if (MineralsCollected > GetHighMineralsCollected())
        {
            SetHighMineralsCollected(MineralsCollected);

            celebrateHiScore = true;
        }

        GameOverUI.Show(celebrateHiScore);
    }


    //==========================================================
    // Minerals HighScore Record STORED

    public int GetHighMineralsCollected()
    {
        return PlayerPrefs.GetInt("HighMineralsCollected", 0);
    }

    //
    public void SetHighMineralsCollected(int minerals)
    {
        PlayerPrefs.SetInt("HighMineralsCollected", minerals);
        PlayerPrefs.Save();
    }
    //===========================================================
}

