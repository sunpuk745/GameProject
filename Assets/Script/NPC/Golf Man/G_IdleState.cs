using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_IdleState : IdleState
{
    private GolfMan golfMan;

    public G_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, GolfMan golfMan) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(golfMan.walkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
