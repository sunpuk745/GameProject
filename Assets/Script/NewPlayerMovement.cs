using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;
    public Animator animator;

    private bool isCrouched = false;

    [Header("Movement")]
    [SerializeField]private float movementAcceleration = 10f;
    [SerializeField]private float maxMoveSpeed = 10f;
    [SerializeField]private float groundMovementDeceleration = 7f;
    private bool canMove = true;
    private float horizontalDirection;

    private bool IsFacingRight = true;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    [Space(5)]

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airDeceleration = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultipier = 5f;
    private bool canJump => Input.GetButtonDown("Jump") && onGround;
    [Space(5)]

    [Header("Attack")]
    [SerializeField] private float AttackForce = 10f;
    private bool canAttack => Input.GetMouseButtonDown(0) && onGround;
    private bool isAttacking = false;
    [Space(5)]


    [Header("Ground Collision Variables")]
    [SerializeField] private Transform groundCheckPoint;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    private bool onGround;
    [Space(10)]

    [Header("Layer Mask")]
    [SerializeField]private LayerMask groundLayer;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update() 
    {
        CheckCollision();
        horizontalDirection = GetInput().x;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        Crouch();
        if ((canJump) && (!isCrouched))
        {
            Jump();

        }
        if ((canAttack) && (!isCrouched))
        {
            Attack();
        }
        if (onGround)
        {
            ApplyGroundDeceleration();
            
        }
        else
        {
            animator.SetBool("IsJumping", true);
            ApplyAirDeceleration();
            FallMultiplier();
        }
    }

    private void FixedUpdate() 
    {
        Move();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void Move()
    {
        if (canMove)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
        }
        if(horizontalDirection > 0 && !IsFacingRight)
            {
                Turn();
            }
            if(horizontalDirection < 0 && IsFacingRight)
            {
                Turn();
            }
    }

    private void ApplyGroundDeceleration()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || changingDirection)
        {
            rb.drag = groundMovementDeceleration;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirDeceleration()
    {
        rb.drag = airDeceleration;
    }

    private void Turn() //Flip character Spirte
	{
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;

		IsFacingRight = !IsFacingRight;
	}
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x , 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultipier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void CheckCollision()
    {
        onGround = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        if (onGround){
            animator.SetBool("IsJumping", false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

    }

    private void Crouch()
    {
        if(Input.GetButton("Crouch") && onGround)
        {
            isCrouched = true;
            animator.SetBool("IsCrouching", true);
            rb.velocity = new Vector2(0,0);
            canMove = false;
        }
        else
        {
            isCrouched = false;
            animator.SetBool("IsCrouching", false);
            canMove = true;
        }
        
    }
}
