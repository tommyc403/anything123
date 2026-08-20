using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // =========================
    // References
    // =========================

    public GameOverUI GameOverUI;
    public ScreenFlash ScreenFlash;

    public GameObject BulletRef;
    public GameObject ExplosionRef;


    // =========================
    // Movement
    // =========================

    public float EnginePower = 10f;
    public float EngineReversePower = 10f;
    public float TurnPower = 10f;

    // Boost
    public float BoostMultiplier = 3f;


    // =========================
    // Firing
    // =========================

    public float FiringRate = 0.66f;
    private float firingTimer = 0f;

    public float BulletSpeed = 100f;


    // =========================
    // Score
    // =========================

    public int Score = 0;
    public int MineralsCollected = 0;


    // =========================
    // Health
    // =========================

    public float HealthMax = 3f;
    private float healthCurrent;

    public float HealthCurrent => healthCurrent;


    // =========================
    // Fuel / Boost
    // =========================

    public float FuelMax = 100f;
    private float fuelCurrent;

    public float FuelCurrent => fuelCurrent;

    // Fuel consumed while boosting
    public float FuelDrainAmount = 20f;
    public float FuelDrainInterval = 1f;

    private float fuelDrainTimer = 0f;

    // Fuel regenerated over time
    public float FuelRechargeAmount = 20f;
    public float FuelRechargeInterval = 20f;

    private float fuelRechargeTimer = 0f;


    // =========================
    // Components
    // =========================

    private Rigidbody2D rigidBody;


    // =========================
    // Initialization
    // =========================

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        healthCurrent = HealthMax;
        fuelCurrent = FuelMax;

        // Start the recharge timer at the full interval.
        fuelRechargeTimer = FuelRechargeInterval;
    }


    // =========================
    // Update
    // =========================

    private void Update()
    {
        UpdateFiring();
        UpdateFuelDrain();
        UpdateFuelRecharge();

        // Turning
        if (Input.GetKey(KeyCode.A))
        {
            ApplyTorque(-1f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            ApplyTorque(1f);
        }

        // Forward / reverse thrust
        float vert = Input.GetAxis("Vertical");

        if (vert > 0f)
        {
            float thrustMultiplier = 1f;

            // Boost while Space is held and fuel remains
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


    // =========================
    // Fuel
    // =========================

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


    private void UpdateFuelRecharge()
    {
        // Don't run the recharge timer while already at maximum fuel.
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


    // =========================
    // Movement
    // =========================

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


    // =========================
    // Firing
    // =========================

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");

        firingTimer -= Time.deltaTime;

        if (isFiring && firingTimer <= 0f)
        {
            FireBullet();

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


    // =========================
    // Health
    // =========================

    public void TakeDamage(float damage)
    {
        Debug.Log("SPACESHIP TAKE DAMAGE: " + damage);

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


    // =========================
    // Minerals
    // =========================

    public void CollectMinerals(int amount)
    {
        MineralsCollected += amount;
    }


    // =========================
    // Explosion / Game Over
    // =========================

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

        if (Score > GetHighScore())
        {
            SetHighScore(Score);

            celebrateHiScore = true;
        }

        GameOverUI.Show(celebrateHiScore);
    }


    // =========================
    // High Score
    // =========================

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }


    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt("Highscore", score);
        PlayerPrefs.Save();
    }
}