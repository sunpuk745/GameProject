using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 enemy;

    public E2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetected stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (enemy.player.transform.position.x > enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x < 0 
        || enemy.player.transform.position.x < enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x > 0)
        {
            entity.Flip();
        }
    }

    public override void Enter()
    {
        base.Enter();

        if (!isPlayerInFleeRange)
        {
            AudioManager.Instance.PlaySFX("WitchCharge", 0.3f);
        }
        
        if (isPlayerInFleeRange && entity.currentHealth <= 50 && usePushMagic)
        {
            usePushMagic = false;
            stateMachine.ChangeState(enemy.rangePushState);
        }
        else if (isPlayerInFleeRange && !usePushMagic)
        {
            stateMachine.ChangeState(enemy.fleeState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        stateData.distanceToPlayer = Vector2.Distance(enemy.transform.position, enemy.player.transform.position);

        if (!DetectPlayerInRange)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (Time.time >= startTime + stateData.magicCastTime)
        {
            stateMachine.ChangeState(enemy.rangeAttackState);
        }

        if (Time.time >= enemy.rangePushState.startTime + enemy.rangePushStateData.magicCooldown)
        {
            usePushMagic = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
