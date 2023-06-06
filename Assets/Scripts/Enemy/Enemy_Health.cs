using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public static Enemy_Health instance;

    [Header("Health System")]
    public int Health;
    private int MaxHealth = 100;

    Animator animator;
    BoxCollider2D boxCollider2D;
    EnemyController enemyController;
    Enemy_Attack_System enemy_Attack_System;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyController = GetComponent<EnemyController>();
        enemy_Attack_System = GetComponent<Enemy_Attack_System>();
        rb2d = GetComponent<Rigidbody2D>();

        instance = this;

        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    public void TakeHealth(int damage)
    {
        Health -= damage;
        StartCoroutine("Hurt");
    }

    IEnumerator Hurt()
    {
        // Play Hurt Animation
        animator.SetBool("IsHurt", true);
        rb2d.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("IsHurt", false);
    }

    void Die()
    {
        // Play Die Animation
        animator.SetBool("IsDead", true);

        // Disable Collision
        boxCollider2D.enabled = false;

        // Disable AI
        enemyController.enabled = false;

        // Disable Enemy_Attack_System
        enemy_Attack_System.enabled = false;

        // Set Gravity 0
        rb2d.gravityScale = 0;

        // Set Linear 80
        rb2d.drag = 80;

        // Destory Object
        Destroy(this.gameObject, 2f);
    }
}
