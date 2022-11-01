using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public Data_Entity entityData;

    public int facingDirection { get; private set; }
    public int lastDamageDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; }
    public AnimationToStateMachine animationToStateMachine { get; private set; }
    public Collider2D enemyCollider { get; private set; }

    [SerializeField]private Transform wallCheck;
    [SerializeField]private Transform ledgeCheck;
    [SerializeField]private Transform playerCheck;
    [SerializeField]private Transform groundCheck;
    public Transform playerPos;

    public float currentHealth;
    private float currentStunResistance;
    private float lastDamageTime;

    private Vector2 velocityWorkspace;

    protected bool isStunned;
    protected bool isDead;
    public bool isImmortal;

    public virtual void Start() 
    {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance;

        isImmortal = false;

        aliveGameObject = transform.Find("Alive").gameObject;
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();
        enemyCollider = aliveGameObject.GetComponent<Collider2D>();
        animationToStateMachine = aliveGameObject.GetComponent<AnimationToStateMachine>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        if (Time.time >= lastDamageTime + entityData.stunRecoveryTimme)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate() 
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkspace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.Ground);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.Ground);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.Ground);
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.minAggroDistance, entityData.Player);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.maxAggroDistance, entityData.Player);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGameObject.transform.right, entityData.closeRangeActionDistance, entityData.Player);
    }
    
    public virtual bool DetectPlayerInRange()
    {
        return Physics2D.OverlapBox((Vector2)playerCheck.position + entityData.playerCheckOffset, entityData.playerDetectRange, 0 ,entityData.Player);
    }

    public virtual bool CheckPlayerInFleeRange()
    {
        return Physics2D.OverlapBox((Vector2)playerCheck.position + entityData.playerFleeRangeOffset, entityData.playerFleeRange, 0 ,entityData.Player);
    }

    public virtual bool CheckPlayerInSpecialSkillRange()
    {
        return Physics2D.OverlapBox((Vector2)playerCheck.position + entityData.specialSkillRangeOffset, entityData.specialSkillRange, 0, entityData.Player);
    }

    public virtual void DamageKnock(float velocity)
    {
        velocityWorkspace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkspace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        if (!isImmortal && !isDead)
        {
            lastDamageTime = Time.time;

            currentHealth -= attackDetails.damageAmount;
            currentStunResistance -= attackDetails.stunDamageAmount;
        }

        if (!isDead && !isImmortal)
        {
            DamageKnock(entityData.DamageKnockSpeed);
        }
        
        if (attackDetails.position.x > aliveGameObject.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else 
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            anim.SetTrigger("dead");
        }
        //Debug.Log(currentHealth);
    }

    public virtual void Turn()
    {
        facingDirection *= -1;
        //.transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGameObject.transform.localScale = new Vector2(aliveGameObject.transform.localScale.x * -1, aliveGameObject.transform.localScale.y);
    }

    public virtual void OnDrawGizmos() 
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAggroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAggroDistance), 0.2f);

        Gizmos.color = entityData.gizmoColor;
        Gizmos.DrawCube((Vector2)playerCheck.position + entityData.playerCheckOffset, entityData.playerDetectRange);

        Gizmos.color = entityData.gizmoFleeColor;
        Gizmos.DrawCube((Vector2)playerCheck.position + entityData.playerFleeRangeOffset, entityData.playerFleeRange);

        Gizmos.color = entityData.gizmoSpecialSkillRangeColor;
        Gizmos.DrawCube((Vector2)playerCheck.position + entityData.specialSkillRangeOffset, entityData.specialSkillRange);
    }
}
