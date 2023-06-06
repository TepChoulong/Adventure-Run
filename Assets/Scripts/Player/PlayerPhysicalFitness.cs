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

    Animator animator;
    [SerializeField] GameObject[] Enemy;
    public int knockback_speed;


    void Awake()
    {
        instance = this;

        Health = MaxHealth;
        Energy = MaxEnergy;

        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (Energy < 100)
        {
            Energy += Time.deltaTime + 1 / 2;   
        }

        if (Health <= 0)
        {
            Die();
        }
    }

    public void TakeHealth(int damage)
    {
        Health -= damage;
        // Play Hurt Animation
        animator.SetTrigger("IsHurt");

        if (PlayerController.instance.facingRight)
        {
            GetComponent<Rigidbody2D>().AddForce(knockback_speed * transform.right, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(-knockback_speed * transform.right, ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        // Play Die Animation
        animator.SetBool("IsDead", true);
        
        // Disable Collision
        GetComponent<CapsuleCollider2D>().enabled = false;

        // Gravity 0
        GetComponent<Rigidbody2D>().gravityScale = 0;

        foreach (GameObject enemy in Enemy)
        {
            // Disable Enemy AI
            enemy.GetComponent<EnemyController>().enabled = false;

            // Diable Enemy Attack
            enemy.GetComponent<Enemy_Attack_System>().enabled = false;
        }

        // Disable Player Controller
        GetComponent<PlayerController>().enabled = false;

        // Disable Player Attack System
        GetComponent<AttackCombat>().enabled = false;

        // Display Death Panel
    }

    public void TakeEnergy(float HowManyEnergyDoesPlayerUseToAttack)
    {
        Energy -= HowManyEnergyDoesPlayerUseToAttack;
    }
}
