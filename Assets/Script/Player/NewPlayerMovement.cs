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
    [SerializeField]private PopUpTextSystem popUpTextSystem;
    // InteractableObejct
    public GameObject interactable;
    [SerializeField]private GameObject hitParticles;
    [SerializeField]private GameObject pauseMenu;
    public bool isPaused;

    [Header("Movement")]
    [SerializeField]private float movementAcceleration = 10f;
    [SerializeField]private float maxMoveSpeed = 10f;
    [SerializeField]private float groundMovementDeceleration = 7f;
    private float moveInput;
    private bool canMove = true;
    private float horizontalDirection;
    private bool IsFacingRight = true;
    private bool changingDirection => (rb.velocity.x > 0f && horizontalDirection < 0f) || (rb.velocity.x < 0f && horizontalDirection > 0f);
    public bool fallThrough => Input.GetKey(KeyCode.S);

    [Header("Jump")]
    [SerializeField]private float jumpForce = 12f;
    [SerializeField]private float airDeceleration = 2.5f;
    [SerializeField]private float fallMultiplier = 8f;
    [SerializeField]private float lowJumpFallMultipier = 5f;
    [SerializeField]private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;
    [Space(10)]

    [Header("Attack")]
    [SerializeField]private float attackCooldown = 1f;
    [SerializeField]private float attackRange;
    [SerializeField]private float attackDamage = 10f;
    [SerializeField]private float stunDamageAmount = 1f;
    private float attackingTime;
    private bool isAttacking;
    private AttackDetails attackDetails;
    [SerializeField] private Transform attackPos;
    [Space(10)]

    [Header("Knockback")]
    [SerializeField]private float knockbackDuration;
    private float knockbackStartTime;
    private bool isKnockback;
    [SerializeField] private Vector2 knockbackSpeed;
    [Space(10)]

    [Header("ParticleEffect")]
    // To use ParticleEffect(walk particle) : footEmission.rateOverTime = 0f; set it to 0f to disable and increase value to enable particle
    public ParticleSystem footsteps;
    [SerializeField]private ParticleSystem.EmissionModule footEmission;
    [Space(10)]

    [Header("Ground Collision Variables")]
    [SerializeField]private Transform groundCheckPoint;
	[SerializeField]private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    private bool onGround;
    [Space(10)]

    [Header("Layer Mask")]
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask whatIsEnemy;

    private void Start() 
    {
        pauseMenu.SetActive(false);

        GetCurrentBuildIndex();

        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();

        footEmission = footsteps.emission;
    }

    private void Update() 
    {

        coyoteTimeCounter -= Time.deltaTime;
        attackingTime -= Time.deltaTime;

        CheckKnockback();
        CheckCollision();
        horizontalDirection = moveInput;
        if (!isPaused)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalDirection));
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

        if (gameManager.currentHealth <= 0)
        {
            animator.SetTrigger("isDead");
        }
    }

    private void FixedUpdate() 
    {
        Move();
    }
    
    // Move

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<float>();
    }

    private void Move()
    {
        if (canMove && !isAttacking && !isKnockback && !gameManager.isDead && !isPaused)
        {
            rb.AddForce(new Vector2(horizontalDirection, 0f) * movementAcceleration);
            if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxMoveSpeed, rb.velocity.y);
            if(moveInput != 0)
            {
                footEmission.rateOverTime = 35f;
            }
            else
            {
                footEmission.rateOverTime = 0f;
            }
            
        }
        if(horizontalDirection > 0 && !IsFacingRight && !isAttacking && !isKnockback && !gameManager.isDead && !isPaused)
            {
                Turn();
            }
        else if(horizontalDirection < 0 && IsFacingRight && !isAttacking && !isKnockback && !gameManager.isDead && !isPaused)
            {
                Turn();
            }
        
        if (isAttacking && !isKnockback || gameManager.isDead)
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

    private void OnJump(InputValue value)
    {
        if (value.isPressed && coyoteTimeCounter > 0 && !isPaused)
        {
            AudioManager.Instance.PlaySFX("Jump");
            rb.velocity = new Vector2(rb.velocity.x , 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator SetZeroCoyoteTimeCounter()
    {
        yield return new WaitForSeconds(0.1f);
        coyoteTimeCounter = 0f;
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
        if (onGround)
        {
            coyoteTimeCounter = coyoteTime;
            animator.SetBool("IsJumping", false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
    }

    private void OnInteract(InputValue value)
    {
        if (value.isPressed && !isPaused)
        {
            if (popUpTextSystem.currentNpc != null)
            {
                popUpTextSystem.PopUp();
            }
        }
    }

// Attack handler

    private void OnAttack(InputValue value)
    {
        if (value.isPressed && attackingTime <= 0 && onGround && !isAttacking && !isKnockback && !isPaused)
        {
            isAttacking = true;
            AudioManager.Instance.PlaySFX("Slash");
            animator.SetTrigger("Attack");
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
        if (!gameManager.isDead && gameManager.currentHealth > 0)
        {
            if (gameManager == null)
            {
                gameManager = FindObjectOfType<GameManager>();
            }
            gameManager.DecreaseHP(attackDetails.damageAmount);

            AudioManager.Instance.PlaySFX("PlayerHit");

            Instantiate(hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        }
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

    private void TriggerRestartSceneAfterDead()
    {
        gameManager.RestartScene();
    }

    private void PlayDeadSound()
    {
        AudioManager.Instance.PlaySFX("PlayerDie", 0.5f);
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
            gameManager.DecreaseHP(100);
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

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void GoToMainMenu()
    {
        if (GetCurrentBuildIndex() <= 7)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(8);
        }
        Time.timeScale = 1f;
    }

    public void ResetScene()
    {
        gameManager.RestartScene();
        Time.timeScale = 1f;
    }
}
