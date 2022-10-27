using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_ChargeState chargeState { get; private set; }
    public E1_FindingPlayerState findingPlayerState { get; private set; }
    public E1_MeleeAttackState meleeAttackState { get; private set; }

    [SerializeField]private Data_IdleState idleStateData;
    [SerializeField]private Data_MoveState moveStateData;
    [SerializeField]private Data_PlayerDetected playerDetectedData;
    [SerializeField]private Data_ChargeState chargeStateData;
    [SerializeField]private Data_FindingPlayerState findingPlayerStateData;
    [SerializeField]private Data_MeleeAttackState meleeAttackStateData;

    [SerializeField]private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        findingPlayerState = new E1_FindingPlayerState(this, stateMachine, "findingPlayer", findingPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

        stateMachine.Initialize(moveState);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
