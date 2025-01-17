using System;
using UnityEngine;

public class PlantStatusIndicator : MonoBehaviour
{
    public static event Action OnPlantHappy;
    public static event Action OnPlantDying;
    public static event Action OnPlantNeedsWater;
    public static event Action OnPlantIsDead;

    private bool PlantIsHappy()
    {
        // Code determining if the plant is happy
        return true;
    }

    private bool PlantIsDying()
    {
        // Code determining if the plant is dying
        return true;
    }

    private bool PlantNeedsWater()
    {
        // Code determining if the plant is dying
        return true;
    }

    private bool PlantIsDead()
    {
        // Code determining if the plant is dead
        return true;
    }
}
