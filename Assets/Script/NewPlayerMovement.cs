using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NewPlayerMovement : MonoBehaviour
{
    [Header("Component")]
    private Rigidbody2D rb;
    public Animator animator;
    private bool isCrouched = false;
    //Teleporter
    private GameObject currentTeleporter;
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

    [Header("Attack")]
    [SerializeField] private float attackingTime = 2f;
    [SerializeField] private float attackCooldown = 1f;
    private bool canAttack = true;
    private bool isAttacking;

    [Header("ParticleEffect")]
    // To use ParticleEffect(walk particle) : footEmission.rateOverTime = 0f; set it to 0f to disable and increase value to enable particle
    [SerializeField] public ParticleSystem footsteps;
    [SerializeField] private ParticleSystem.EmissionModule footEmission;

    [Header("Camera Shake")]
    // To use Camera Shake : CinemachineShake.Instance.ShakeCamera(intensity, shakeTime);
    [SerializeField] private float intensity = 5f;
    [SerializeField] private float shakeTime = 0.3f;

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
        footEmission = footsteps.emission;
    }

    private void Update() 
    {
 
        if(isAttacking)
        {
            return;
        }
        
        CheckCollision();
        horizontalDirection = GetInput().x;
        animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
        Crouch();
        if (Input.GetMouseButtonDown(0) && canAttack && onGround)
        {
            StartCoroutine(Attack());
        }
        if ((canJump) && (!isCrouched))
        {
            Jump();
        }

        // Teleport
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
                transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
            }
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
        if(isAttacking)
        {
            return;
        }

        Move();
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        
    }

    // Move
    private void Move()
    {
        if (canMove)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                footEmission.rateOverTime = 35f;
            }
            else{
                footEmission.rateOverTime = 0f;
            }
            
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

// Attack
    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        rb.velocity = new Vector2(0,0);
        animator.SetTrigger("Attack");
        CinemachineShake.Instance.ShakeCamera(intensity, shakeTime);
        yield return new WaitForSeconds(attackingTime);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Teleport
        if (collision.CompareTag("Teleporter"))
        {
            interactable.SetActive(true);
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Teleport
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            currentTeleporter = null;
            interactable.SetActive(false);
        }
    }

}
