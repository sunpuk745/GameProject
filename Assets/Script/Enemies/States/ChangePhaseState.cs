using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhaseState : State
{
    protected Data_ChangePhaseState stateData;

    protected bool isAnimationFinished;

    public ChangePhaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_ChangePhaseState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        entity.animationToStateMachine.changePhaseState = this;
        isAnimationFinished = false;
        entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void FinishChangePhaseAnimation()
    {
        isAnimationFinished = true;
    }
}
