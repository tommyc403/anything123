using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroids : Asteroid
{

    //Serialize = Adjustable in inspector menu
    [SerializeField] private GameObject GoldChunk;
    [SerializeField] private GameObject ValuableChunk;
    [SerializeField] private GameObject RocketFuelChunk;
    [SerializeField] private GameObject RestorativeOreChunk;

    // NEW BAR FOR EDITING SPAWN RATES
    [SerializeField, Range(0f, 100f)] private float GoldChance = 90f;
    [SerializeField, Range(0f, 100f)] private float ValuableChance = 5f;
    [SerializeField, Range(0f, 100f)] private float RocketFuelChance = 3f;
    [SerializeField, Range(0f, 100f)] private float RestorativeOreChance = 2f;

    protected override void CreateAsteroidChunk()
    {

        // GAMBLING (Roll for chunk variant off SmallAss)
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
        // If no chunk ref rolled, do nada (Connect Black Hole?)***
        if (chunkRef == null)
        {
            return;
        }
        SpawnChunk(chunkRef);
    }
}