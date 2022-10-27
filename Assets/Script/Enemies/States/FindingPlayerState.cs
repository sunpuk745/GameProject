using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindingPlayerState : State
{
    protected Data_FindingPlayerState stateData;

    protected bool turnImmediately;
    protected bool isPlayerInMinAggroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    public FindingPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_FindingPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAggroRange = entity.CheckPlayerInMinAggroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAllTurnsDone = false;
        isAllTurnsTimeDone = false;

        lastTurnTime = startTime;
        amountOfTurnsDone = 0;

        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (turnImmediately)
        {
            entity.Turn();
            lastTurnTime = Time.time;
            amountOfTurnsDone ++;
            turnImmediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurn && !isAllTurnsDone)
        {
            entity.Turn();
            lastTurnTime = Time.time;
            amountOfTurnsDone ++;
        }

        if (amountOfTurnsDone >= stateData.turnAmount)
        {
            isAllTurnsDone = true;
        }

        if (Time.time >= lastTurnTime + stateData.timeBetweenTurn && isAllTurnsDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetTurnImmediately(bool turn)
    {
        turnImmediately = turn;
    }
}
