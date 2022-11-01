using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airburst : MonoBehaviour
{
    private AttackDetails attackDetails;
    [SerializeField]private NewPlayerMovement newPlayerMovement;
    [SerializeField]private Entity enemy;

    private float startXPos;
    private int attackedDirection;
    [SerializeField] private float airPushRadius;

    private bool isCasted;
    private bool isMagicFinished;
    private bool Reversed;

    [SerializeField] private LayerMask whatIsPlayer;

    [SerializeField] private Transform pushPos;

    private Animator anim;

    private void Start() 
    {
        Reversed = false;
        enemy = GameObject.Find("Enemy2").GetComponent<Entity>();
        newPlayerMovement = GameObject.Find("Player").GetComponent<NewPlayerMovement>();
        anim = GetComponent<Animator>();
        isMagicFinished = false;

        startXPos = transform.position.x;
        if (enemy.facingDirection == -1)
        {
            Reversed = true;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void FixedUpdate() 
    {
        if (isMagicFinished)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerAttack()
    {
        Collider2D magicHit = Physics2D.OverlapCircle(pushPos.position, airPushRadius, whatIsPlayer);
        if (magicHit)
        {
            magicHit.transform.SendMessage("Damage", attackDetails);
            if (Reversed)
            {
                newPlayerMovement.Knockback(-1, 100f);
            }
            else if (!Reversed)
            {
                newPlayerMovement.Knockback(1, 100f);
            }
            
        }
    }

    public void FireMagic(float damage)
    {
        attackDetails.damageAmount = damage;
    }

    public void FinishAttack()
    {
        isMagicFinished = true;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(pushPos.position, airPushRadius);
    }
}
