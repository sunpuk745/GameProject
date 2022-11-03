using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MeleeAttack2Phase2State : MeleeAttackState
{
    private Enemy3 enemy;

    public E3_MeleeAttack2Phase2State(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        AudioManager.Instance.PlaySFX("E3Atk2", 0.5f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();

        GameObject.Instantiate(enemy.archer, enemy.aliveGameObject.transform.position - enemy.archerOffset, enemy.aliveGameObject.transform.rotation);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.move2State);
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
