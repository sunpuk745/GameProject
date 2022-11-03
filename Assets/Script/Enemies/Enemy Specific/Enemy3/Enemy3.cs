using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_MoveState moveState { get; private set; }
    public E3_Move2State move2State { get; private set; }
    public E3_MeleeAttack1State meleeAttack1State { get; private set; }
    public E3_MeleeAttack1Phase2State meleeAttack1Phase2State { get; private set; }
    public E3_MeleeAttack2State meleeAttack2State { get; private set; }
    public E3_MeleeAttack2Phase2State meleeAttack2Phase2State { get; private set; }
    public E3_SpecialAttackState specialAttackState { get; private set; }
    public E3_SpecialAttackPhase2State specialAttackPhase2State { get; private set; }
    public E3_ChangePhaseState changePhaseState { get; private set; }
    public E3_WarpAttackState warpAttackState { get; private set; }
    public E3_DeadState deadState { get; private set; }

    [SerializeField]private Data_MoveState moveStateData;
    [SerializeField]private Data_MoveState move2StateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack1StateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack1Phase2StateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack2Phase2StateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack2StateData;
    [SerializeField]private Data_ChangePhaseState changePhaseStateData;
    [SerializeField]private Data_DeadState deadStateData;

    public Data_MeleeAttackState specialAttackStateData;
    public Data_MeleeAttackState specialAttackPhase2StateData;
    public Data_MeleeAttackState warpAttackStateData;

    public GameObject player;
    public GameObject archer;
    [SerializeField]private GameObject hitParticle;
    [SerializeField]private GameObject hitParticle2;

    public bool canUseSpecialAttack;
    public bool canUseWarpAttack;
    protected bool alreadyChangedPhase;

    public Vector3 archerOffset;
    public Vector3 archerOffsetSpecial;

    [SerializeField]private Transform meleeAttack1Pos;
    [SerializeField]private Transform meleeAttack2Pos;
    [SerializeField]private Transform specialAttackPos;

    public override void Start()
    {
        base.Start();

        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        move2State = new E3_Move2State(this, stateMachine, "move2", move2StateData, this);
        meleeAttack1State = new E3_MeleeAttack1State(this, stateMachine, "atk1", meleeAttack1Pos, meleeAttack1StateData, this);
        meleeAttack1Phase2State = new E3_MeleeAttack1Phase2State(this, stateMachine, "atk1Phase2", meleeAttack1Pos, meleeAttack1Phase2StateData, this);
        meleeAttack2State = new E3_MeleeAttack2State(this, stateMachine, "atk2", meleeAttack2Pos, meleeAttack2StateData, this);
        meleeAttack2Phase2State = new E3_MeleeAttack2Phase2State(this, stateMachine, "atk2Phase2", meleeAttack2Pos, meleeAttack2Phase2StateData, this);
        specialAttackState = new E3_SpecialAttackState(this, stateMachine, "specialAtk", specialAttackPos, specialAttackStateData, this);
        specialAttackPhase2State = new E3_SpecialAttackPhase2State(this, stateMachine, "specialAtkPhase2", specialAttackPos, specialAttackPhase2StateData, this);
        changePhaseState = new E3_ChangePhaseState(this, stateMachine, "changePhase", changePhaseStateData, this);
        warpAttackState = new E3_WarpAttackState(this, stateMachine, "warpAtk", meleeAttack1Pos, warpAttackStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(moveState);
        canUseWarpAttack = false;
        canUseSpecialAttack = false;
        alreadyChangedPhase = false;
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (!isImmortal && !isDead)
        {
            if (!alreadyChangedPhase)
            {
                Instantiate(hitParticle, aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            }
            else
            {
                Instantiate(hitParticle2, aliveGameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            }
        }
        
        if (currentHealth <= 100 && !alreadyChangedPhase)
        {
            alreadyChangedPhase = true;
            stateMachine.ChangeState(changePhaseState);
        }

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(meleeAttack1Pos.position, meleeAttack1StateData.attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(meleeAttack2Pos.position, meleeAttack2StateData.attackRadius);
        // Gizmos.color = specialAttackStateData.gizmoSpecialAttackColor;
        // Gizmos.DrawCube((Vector2)specialAttackPos.position + specialAttackStateData.specialAttackRangeOffset, specialAttackStateData.specialAttackRange);
    }
}
