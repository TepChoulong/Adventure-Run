using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack_System : MonoBehaviour
{
    public static Enemy_Attack_System instance;

    [Header("Number")]
    public float Attack_Range;
    public int Attack_Damage;
    [Space]

    [Header("True&False")]
    public bool AllowAttack2 = false;
    public bool FacingRight = true;
    [Space]

    [Header("Transform")]
    [SerializeField] Transform CastPoint;
    [SerializeField] Transform EnemyAttackPoint;
    [SerializeField] Transform Player;
    [Space]

    [Header("Components")]
    [SerializeField] LayerMask WhatToDetect;
    [SerializeField] Animator animator;

    [Header("GameObject")]
    [SerializeField] GameObject Player_Object;
    
    private Collider2D Hit_Player;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();
        Player_Object = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Hit_Player = Physics2D.OverlapCircle(EnemyAttackPoint.position, Attack_Range);
        
    }

    private void FixedUpdate() {
        // Methods
        Enemy_Facing();

        if(CanSeePlayer(Attack_Range))
        {
            // Play attack animation
            animator.SetTrigger("IsAttack");
            
            animator.SetBool("AllowWalk", false);
        }
    }

    public void TakePlayerHelath()
    {
        // Take player health
        PlayerPhysicalFitness.instance.TakeHealth(Attack_Damage);
    }   

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

            Debug.DrawLine(CastPoint.position, endpos, Color.yellow);
        }
        else
        {
            Debug.DrawLine(CastPoint.position, endpos, Color.green);
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

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(EnemyAttackPoint.position, Attack_Range);
    }
}
