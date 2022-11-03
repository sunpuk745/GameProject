using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MeleeAttack1State : MeleeAttackState
{
    private Enemy3 enemy;

    public E3_MeleeAttack1State(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (Time.time >= enemy.specialAttackState.startTime + enemy.specialAttackStateData.specialAttackCooldown)
        {
            enemy.canUseSpecialAttack = true;
        }
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.Instance.PlaySFX("E3Atk1", 0.5f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.meleeAttack2State);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
