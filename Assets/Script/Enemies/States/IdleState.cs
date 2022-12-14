using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected Data_IdleState stateData;

    protected bool TurnAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAggroRange;
    protected bool DetectPlayerInRange;

    protected float idleTime;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
        DetectPlayerInRange = entity.DetectPlayerInRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (TurnAfterIdle)
        {
            entity.Turn();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnAfterIdle(bool turn)
    {
        TurnAfterIdle = turn;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
}
