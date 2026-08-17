using UnityEngine;

public class SmallAsteroids : Asteroid
{
    [SerializeField] private GameObject GoldChunk;
    [SerializeField] private GameObject ValuableChunk;
    [SerializeField] private GameObject RocketFuelChunk;
    [SerializeField] private GameObject RestorativeOreChunk;

    [SerializeField, Range(0f, 100f)] private float GoldChance = 90f;
    [SerializeField, Range(0f, 100f)] private float ValuableChance = 5f;
    [SerializeField, Range(0f, 100f)] private float RocketFuelChance = 3f;
    [SerializeField, Range(0f, 100f)] private float RestorativeOreChance = 2f;

    protected override void CreateAsteroidChunk()
    {
        float roll = Random.Range(0f, 100f);

        GameObject chunkRef;

        if (roll < GoldChance)
        {
            chunkRef = GoldChunk;
        }
        else if (roll < GoldChance + ValuableChance)
        {
            chunkRef = ValuableChunk;
        }
        else if (roll < GoldChance + ValuableChance + RocketFuelChance)
        {
            chunkRef = RocketFuelChunk;
        }
        else
        {
            chunkRef = RestorativeOreChunk;
        }

        if (chunkRef == null)
        {
            Debug.LogError("Selected chunk prefab is NULL!");
            return;
        }

        SpawnChunk(chunkRef);
    }
}