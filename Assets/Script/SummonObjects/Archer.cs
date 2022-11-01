using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private AttackDetails attackDetails;

    [SerializeField]private Entity enemy;
    private Animator anim;

    private float startXPos;
    private float startYPos;
    private float damage = 10f;

    public Color attackGizmoColor = Color.red;
    public Vector2 attackRange = Vector2.one;
    public Vector2 attackRangeOffset = Vector2.zero;

    private bool isAttackFinished;
    private bool Reversed;

    [SerializeField]private LayerMask whatIsPlayer;

    [SerializeField]private Transform attackPos;

    private void Start() 
    {
        enemy = GameObject.Find("Enemy3").GetComponent<Entity>();
        anim = GetComponent<Animator>();

        isAttackFinished = false;
        Reversed = false;

        startXPos = transform.position.x;
        startYPos = transform.position.y;

        if (enemy.facingDirection == -1)
        {
            Reversed = true;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void FixedUpdate() 
    {
        if (isAttackFinished)
        {
            Destroy(gameObject);
        }
    }

    public void TriggerAttack()
    {
        Collider2D[] beamHit = Physics2D.OverlapBoxAll((Vector2)attackPos.position + attackRangeOffset, attackRange, 0, whatIsPlayer);

        DamageSet();
        
        foreach (Collider2D collider in beamHit)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    private void DamageSet()
    {
        attackDetails.damageAmount = damage;
    }

    public void FinishAttack()
    {
        isAttackFinished = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = attackGizmoColor;
        Gizmos.DrawCube((Vector2)attackPos.position + attackRangeOffset, attackRange);
    }
    
}
