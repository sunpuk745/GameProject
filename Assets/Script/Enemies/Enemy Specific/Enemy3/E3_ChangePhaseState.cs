using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_ChangePhaseState : ChangePhaseState
{
    private Enemy3 enemy;

    public E3_ChangePhaseState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_ChangePhaseState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        AudioManager.Instance.PlaySFX("E3ChangePhase", 0.3f);

        entity.isImmortal = true;
        enemy.canUseSpecialAttack = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishChangePhaseAnimation()
    {
        base.FinishChangePhaseAnimation();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            enemy.isImmortal = false;
            stateMachine.ChangeState(enemy.move2State);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
