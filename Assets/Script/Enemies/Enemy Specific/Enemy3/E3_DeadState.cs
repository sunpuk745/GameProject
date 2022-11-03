using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_DeadState : DeadState
{
    private Enemy3 enemy;

    public E3_DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

    public override void FinishDead()
    {
        base.FinishDead();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
