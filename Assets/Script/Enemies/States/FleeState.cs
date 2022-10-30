using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : State
{
    protected Data_FleeState stateData;

    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isFleeTimeOver;

    public FleeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_FleeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

    public override void Enter()
    {
        base.Enter();

        isFleeTimeOver = false;
        entity.Flip();
        entity.SetVelocity(stateData.fleeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.fleeTime)
        {
            isFleeTimeOver = true;
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            //entity.aliveGameObject.transform.position = new Vector2(entity.aliveGameObject.transform.position.x + stateData.teleportDistance * -1, entity.aliveGameObject.transform.position.y);
            entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
