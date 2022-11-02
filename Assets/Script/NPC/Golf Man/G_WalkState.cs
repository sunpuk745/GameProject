using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_WalkState : MoveState
{
    private GolfMan golfMan;

    public G_WalkState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, GolfMan golfMan) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.golfMan = golfMan;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isDetectingWall || !isDetectingLedge)
        {
            golfMan.idleState.SetTurnAfterIdle(true);
            stateMachine.ChangeState(golfMan.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
