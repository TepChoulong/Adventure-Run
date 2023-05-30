using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCombat : MonoBehaviour
{
    public static AttackCombat instance;

    [SerializeField] Transform Attack_Point;
    [SerializeField] float Attack_Range = 1.6f;
    [SerializeField] LayerMask Enemy_Layer;
    public float Knockback_Speed = 1;
    private Collider2D[] Hit_Enemy;

    private void Awake() {
        instance = this;
    }

    private void Update() {
        // Check Enemy
        Hit_Enemy = Physics2D.OverlapCircleAll(Attack_Point.position, Attack_Range, Enemy_Layer);
    }

    public void AttackEnemy()
    {
        // Damage Them
        foreach (Collider2D enemy in Hit_Enemy)
        {
            Debug.Log("This is " + enemy.name);

            // Loss Health
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(Attack_Point.position, Attack_Range);
    }

    public void Knockback_Enemy()
    {
        foreach (Collider2D enemy in Hit_Enemy)
        {
            if (PlayerController.instance.facingRight.Equals(true))
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(transform.right * Knockback_Speed, ForceMode2D.Impulse);
            }
            else if (PlayerController.instance.facingRight.Equals(false))
            {
                enemy.GetComponent<Rigidbody2D>().AddForce(transform.right * -Knockback_Speed, ForceMode2D.Impulse);
            }
        }
        
    }
}
