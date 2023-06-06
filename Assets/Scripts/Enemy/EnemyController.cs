using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [Header("Boolean Variable")]
    public bool FacingRight = true; 
    [SerializeField] bool IsArgo = false;
    [SerializeField] bool IsSearching = false;
    [Space]
    
    [Header("Tranform Variable")]
    [SerializeField] Transform Player;
    [SerializeField] Transform CastPoint;

    [Header("Float Variable")]
    [SerializeField] float Enemy_Range; 
    [SerializeField] float Enemy_Move_Speed;
    public static float DistanceToPlayer;
    [Space]

    [Header("LayerMask Variable")]
    [SerializeField] LayerMask WhatToDetect;
    [Space]

    [Header("Component Variable")]
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Animator animator;

/* ==================================================================================== */

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check distance from enemy to player
        DistanceToPlayer = Vector2.Distance(transform.position, Player.position);

        // Method
        EnemyTracking();
        Enemy_Facing();
    }

/* ==================================================================================== */

    void EnemyTracking()
    {
        if (CanSeePlayer(Enemy_Range))
        {
            IsArgo = true;
        }
        else
        {
            if (IsArgo)
            {
                if (!IsSearching)
                {
                    IsSearching = true;
                    Invoke("StopChase", 3);
                }
            }
        }

        if (IsArgo)
        {
            Chase();
        }
    }

/* ==================================================================================== */

    void Chase()
    {
        if (transform.position.x > Player.position.x)
        {
            // Enemy is on the right side, so move left
            rb2d.velocity = new Vector2(-Enemy_Move_Speed * Time.deltaTime, rb2d.velocity.y);

            // Walk Left Animation
            animator.SetBool("AllowWalk", true);
        }
        
        if (transform.position.x < Player.position.x)
        {
            // Enemy is on the left side, so move Right
            rb2d.velocity = new Vector2(Enemy_Move_Speed * Time.deltaTime, rb2d.velocity.y);

            // Walk Right Animation
            animator.SetBool("AllowWalk", true);
        }
        
        
    }
    void StopChase()
    {
        IsArgo = false;
        IsSearching = false;
        rb2d.velocity = new Vector2(0,0);
        animator.SetBool("AllowWalk", false);
    }

/* ==================================================================================== */

    // Attack Method

/* ==================================================================================== */

    bool CanSeePlayer(float distance)
    {
        bool value = false;
        float castDistance = distance;

        if (!FacingRight)
        {
            castDistance = distance * -1;
        }

        Vector2 endpos = CastPoint.position + Vector3.right * castDistance;

        RaycastHit2D hit = Physics2D.Linecast(CastPoint.position, endpos, WhatToDetect);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                value = true;
            }
            else
            {
                value = false;
            }

            Debug.DrawLine(CastPoint.position, endpos, Color.cyan);
        }
        else
        {
            Debug.DrawLine(CastPoint.position, endpos, Color.red);
        }

        return value;
    }

    void Enemy_Facing()
    {
        if (transform.position.x > Player.position.x)
        {
            // Enemy is on the right side, so face left
            Vector3 Scaler = transform.localScale;
            Scaler.x = -0.7507975f;
            transform.localScale = Scaler;
            FacingRight = false;
        }
        
        if (transform.position.x < Player.position.x)
        {
            // Enemy is on the left side, so face Right
            Vector3 Scaler = transform.localScale;
            Scaler.x = 0.7507975f;
            transform.localScale = Scaler;
            FacingRight = true;
        }
    }
}
