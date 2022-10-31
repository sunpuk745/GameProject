using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_MoveState moveState { get; private set; }
    public E3_MeleeAttack1State meleeAttack1State { get; private set; }
    public E3_MeleeAttack2State meleeAttack2State { get; private set; }
    public E3_SpecialAttackState specialAttackState { get; private set; }

    [SerializeField]private Data_MoveState moveStateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack1StateData;
    [SerializeField]private Data_MeleeAttackState meleeAttack2StateData;
    public Data_MeleeAttackState specialAttackStateData;

    public GameObject player;

    public bool canUseSpecialAttack;

    [SerializeField]private Transform meleeAttack1Pos;
    [SerializeField]private Transform meleeAttack2Pos;
    [SerializeField]private Transform specialAttackPos;

    public override void Start()
    {
        base.Start();

        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        meleeAttack1State = new E3_MeleeAttack1State(this, stateMachine, "atk1", meleeAttack1Pos, meleeAttack1StateData, this);
        meleeAttack2State = new E3_MeleeAttack2State(this, stateMachine, "atk2", meleeAttack2Pos, meleeAttack2StateData, this);
        specialAttackState = new E3_SpecialAttackState(this, stateMachine, "specialAtk", specialAttackPos, specialAttackStateData, this);

        stateMachine.Initialize(moveState);
        canUseSpecialAttack = false;
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
