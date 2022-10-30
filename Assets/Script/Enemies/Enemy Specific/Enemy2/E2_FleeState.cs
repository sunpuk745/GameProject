using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_FleeState : FleeState
{
    private Enemy2 enemy;

    public E2_FleeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_FleeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isFleeTimeOver)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isDetectingWall || !isDetectingLedge)
        {
            if (entity.facingDirection == 1)
            {
                enemy.transform.position = new Vector2(entity.playerPos.position.x - stateData.teleportDistance, entity.playerPos.position.y + 1.5f);
            }
            else if (entity.facingDirection == -1)
            {
                enemy.transform.position = new Vector2(entity.playerPos.position.x + stateData.teleportDistance, entity.playerPos.position.y + 1.5f);
            }
            
            entity.SetVelocity(0f);
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
