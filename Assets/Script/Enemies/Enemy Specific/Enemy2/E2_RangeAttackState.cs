using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangeAttackState : RangeAttackState
{
    private Enemy2 enemy;

    public E2_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_RangeAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        AudioManager.Instance.PlaySFX("IceForm");

        Magic = GameObject.Instantiate(stateData.Magic, attackPosition.position + stateData.magicSpawnOffset2, attackPosition.rotation);
        Magic = GameObject.Instantiate(stateData.Magic, attackPosition.position + stateData.magicSpawnOffset3, attackPosition.rotation);
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

        if (Time.time >= startTime + stateData.idleTimeBeforeChangeState)
        {
            stateMachine.ChangeState(enemy.idleState);
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
