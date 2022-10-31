using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class NewPlayerMovement : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;
    public Animator animator;
    private GameManager gameManager;
    private bool isCrouched = false;
    // InteractableObejct
    public GameObject interactable;
    //One-way platform
    public bool fallThrough => Input.GetKey(KeyCode.S);

    [Header("Movement")]
    [SerializeField]private float movementAcceleration = 10f;
    [SerializeField]private float maxMoveSpeed = 10f;
    [SerializeField]private float groundMovementDeceleration = 7f;
    private bool canMove = true;
    private float horizontalDirection;
    private bool IsFacingRight = true;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float airDeceleration = 2.5f;
    [SerializeField] private float fallMultiplier = 8f;
    [SerializeField] private float lowJumpFallMultipier = 5f;
    private bool canJump => Input.GetButtonDown("Jump") && onGround;
    [Space(10)]

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float stunDamageAmount = 1f;
    private float attackingTime;
    //private int attackedDirection;
    private bool isAttacking;
    private AttackDetails attackDetails;
    [SerializeField] private Transform attackPos;
    [Space(10)]

    [Header("Knockback")]
    [SerializeField] private float knockbackDuration;
    private float knockbackStartTime;
    private bool isKnockback;
    [SerializeField] private Vector2 knockbackSpeed;
    [Space(10)]

    [Header("ParticleEffect")]
    // To use ParticleEffect(walk particle) : footEmission.rateOverTime = 0f; set it to 0f to disable and increase value to enable particle
    [SerializeField] public ParticleSystem footsteps;
    [SerializeField] private ParticleSystem.EmissionModule footEmission;
    [Space(10)]

    [Header("Ground Collision Variables")]
    [SerializeField] private Transform groundCheckPoint;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    private bool onGround;
    [Space(10)]

    [Header("Layer Mask")]
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask whatIsEnemy;

    private void Start() 
    {
        GetCurrentBuildIndex();

        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        footEmission = footsteps.emission;
    }

    private void Update() 
    {  
        CheckAttack();
        CheckKnockback();
        CheckCollision();
        horizontalDirection = GetInput().x;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        Crouch();

        if ((canJump) && (!isCrouched))
        {
            Jump();
        }
        
        if (onGround)
        {
            ApplyGroundDeceleration();
        }
        else
        {
            footEmission.rateOverTime = 0f;
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

    // Move
    private void Move()
    {
        if (canMove && !isAttacking && !isKnockback)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                footEmission.rateOverTime = 35f;
            }
            else
            {
                footEmission.rateOverTime = 0f;
            }
            
        }
        if(horizontalDirection > 0 && !IsFacingRight && !isAttacking && !isKnockback)
            {
                Turn();
            }
            if(horizontalDirection < 0 && IsFacingRight && !isAttacking && !isKnockback)
            {
                Turn();
            }
        
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
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

    private void Turn()
	{
		Vector3 scale = transform.localScale; 
		scale.x *= -1;
		transform.localScale = scale;
		IsFacingRight = !IsFacingRight;
	}
    
    // Jump
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

    // Ground Check
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

    // Crouch
    private void Crouch()
    {
        if(Input.GetButton("Crouch") && onGround)
        {
            isCrouched = true;
            footEmission.rateOverTime = 0f;
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

// Attack handler

    private void CheckAttack()
    {
        if (attackingTime <= 0 && onGround && !isAttacking && !isKnockback)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isAttacking = true;
                animator.SetTrigger("Attack");
            }
        }
        else 
        {
            attackingTime -= Time.deltaTime;
        }
    }

    private void AttackCheck()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        
        attackDetails.damageAmount = attackDamage;
        attackDetails.stunDamageAmount = stunDamageAmount;
        attackingTime = attackCooldown;

        foreach (Collider2D collider in enemiesToDamage)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
        }
    }

    private void FinishAttack()
    {
        isAttacking = false;
    }

    //Take damage
    private void Damage(AttackDetails attackDetails)
    {
        gameManager.DecreaseHP(attackDetails.damageAmount);

        // if (attackDetails.position.x < transform.position.x)
        // {
        //     attackedDirection = 1;
        // }
        // else
        // {
        //     attackedDirection = -1;
        // }

        //Knockback(attackedDirection);
    }

    public void Knockback(int direction, float knockDistance)
    {
        isKnockback = true;
        knockbackStartTime = Time.time;
        rb.velocity = new Vector2((knockbackSpeed.x + knockDistance) * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && isKnockback)
        {
            isKnockback = false;
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Interactable"))
        {
            interactable.SetActive(true);
        }

        if (collision.CompareTag("Hazard")) 
        {
            Debug.Log("Player Hit");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactable.SetActive(false);
        }
    }

    public int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
