using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected Data_DeadState stateData;

    protected bool isAnimationFinished;

    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData) : base(entity, stateMachine, animBoolName)
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

        //entity.gameObject.SetActive(false);
        entity.animationToStateMachine.deadState = this;
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
        
        if (isAnimationFinished)
        {
            entity.gameObject.SetActive(false);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void FinishDead()
    {
        isAnimationFinished = true;
    }
}
