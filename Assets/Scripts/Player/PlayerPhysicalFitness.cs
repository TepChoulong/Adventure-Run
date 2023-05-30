using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicalFitness : MonoBehaviour
{
    public static PlayerPhysicalFitness instance;

    [Header("Health System")]
    public int Health;
    private int MaxHealth = 100;
    [Space]

    [Header("Energy System")]
    public float Energy; // for attacking
    private float MaxEnergy = 100f;


    void Awake()
    {
        instance = this;

        Health = MaxHealth;
        Energy = MaxEnergy;
    }

    void Update() 
    {
        if (Energy < 100)
        {
            Energy += Time.deltaTime + 1 / 2;   
        }
    }

    public void TakeHealth(int damage)
    {
        Health -= damage;
    }

    void Die()
    {
        // Play Die Animation
        // Disable Collision
        // Display Death Panel
    }

    public void TakeEnergy(float HowManyEnergyDoesPlayerUseToAttack)
    {
        Energy -= HowManyEnergyDoesPlayerUseToAttack;
    }
}
