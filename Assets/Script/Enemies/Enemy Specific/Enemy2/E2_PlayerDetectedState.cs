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

        stateData.distanceToPlayer = Vector2.Distance(enemy.transform.position, entity.playerPos.position);

        if (entity.playerPos.position.x > enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x < 0 
        || entity.playerPos.position.x < enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x > 0)
        {
            entity.Flip();
        }
        else if (!DetectPlayerInRange)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (isPlayerInFleeRange)
        {
            stateMachine.ChangeState(enemy.fleeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
