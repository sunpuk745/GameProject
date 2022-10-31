using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MeleeAttack2State : MeleeAttackState
{
     private Enemy3 enemy;

    public E3_MeleeAttack2State(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
            if (isPlayerInMeleeRange)
            {
                if (enemy.canUseSpecialAttack)
                {
                    enemy.canUseSpecialAttack = false;
                    stateMachine.ChangeState(enemy.specialAttackState);
                }
                else
                {
                    stateMachine.ChangeState(enemy.meleeAttack1State);
                }
            }
            else
            {
                stateMachine.ChangeState(enemy.moveState);
            }
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
