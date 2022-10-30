using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState { get; private set; }
    public E2_PlayerDetectedState playerDetectedState { get; private set; }
    public E2_FleeState fleeState { get; private set; }
    public E2_RangeAttackState rangeAttackState { get; private set; }
    public E2_RangePushState rangePushState { get; private set; }

    [SerializeField]private Data_IdleState idleStateData;
    [SerializeField]private Data_PlayerDetected playerDetectedStateData;
    [SerializeField]private Data_FleeState fleeStateData;
    [SerializeField]private Data_RangeAttackState rangeAttackStateData;
    public Data_RangeAttackState rangePushStateData;

    [SerializeField]private Transform pushAttackPos;
    [SerializeField]private Transform rangeAttackPos;

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        fleeState = new E2_FleeState(this, stateMachine, "flee", fleeStateData, this);
        rangeAttackState = new E2_RangeAttackState(this, stateMachine, "rangeAttack", rangeAttackPos ,rangeAttackStateData, this);
        rangePushState = new E2_RangePushState(this, stateMachine, "pushAttack", pushAttackPos, rangePushStateData, this);

        stateMachine.Initialize(idleState);
    }
}
