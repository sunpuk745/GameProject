using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;

    public Data_Entity entityData;

    public int facingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject aliveGameObject { get; private set; }

    [SerializeField]private Transform wallCheck;
    [SerializeField]private Transform ledgeCheck;

    private Vector2 velocityWorkspace;

    public virtual void Start() 
    {
        aliveGameObject = transform.Find("Alive").gameObject;
        rb = aliveGameObject.GetComponent<Rigidbody2D>();
        anim = aliveGameObject.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
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

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGameObject.transform.right, entityData.wallCheckDistance, entityData.Ground);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.Ground);
    }

    public virtual void Turn()
    {
        facingDirection *= -1;
        aliveGameObject.transform.Rotate(0f, 180f, 0f);
    }
}
