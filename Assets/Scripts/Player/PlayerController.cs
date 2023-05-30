using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
     /*
    Note: 
        1. If you want to make your GameObject stop shaking of jitter you should change the interpolate mode in the rigibody2D component to "interpolate"
        (in the inspector of that GameObject).
    */

    public static PlayerController instance;
    
    //Movement System
    public float speed;
    Rigidbody2D rb2d;
    public float h_Move;
    float v_Move;
    Animator animator;
    //Jump System
    public float jumpspeed;
    // Grounded System
    public Transform GroundCheck;
    public LayerMask Ground;
    bool IsGrounded;
    public float GroundRange = 0.3f;
    //Flip System
    public bool facingRight = true;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpAnimationEvent();
        
        h_Move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(h_Move));

        v_Move = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded)
            {
                Jump();
            }
        }
        
    }

    void FixedUpdate()
    {
        Move();
        IsGroundCheck();

        if (facingRight == false && h_Move > 0)
        {
            FlipPlayer();
        }
        else if (facingRight == true && h_Move < 0)
        {
            FlipPlayer();
        }
    }

    //Method

    void Move()
    {
        rb2d.velocity = new Vector2(h_Move * speed * Time.fixedDeltaTime, rb2d.velocity.y);
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed * Time.fixedDeltaTime);
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void IsGroundCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundRange, Ground);
    }
    
    void JumpAnimationEvent()
    {
        if (rb2d.velocity.y > 0.01)
        {
            animator.SetBool("IsJump", true);
            animator.SetBool("IsFall", false);
        }
        
        if (rb2d.velocity.y < 0.01)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", true);
        }
        
        if (IsGrounded)
        {
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
        }
        
        /*
        Note : It is use only one animation that contain the jump and fall animation inside it. This script require only one animation to make it run.
        
        if (rb2.velocity.y > 0.01)
        {
            animator.SetBool("name of the jump animation parameter", true);
        }
        
        if (IsGround)
        {
            animator.SetBool("name of the jump animation parameter", false);
        }
        */
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundRange);
    }
}
