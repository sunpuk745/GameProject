using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_WarpState : MeleeAttackState
{
    private Enemy2 enemy;

    public E2_WarpState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        entity.isImmortal = true;
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
            if (entity.facingDirection == 1)
            {
                enemy.aliveGameObject.transform.position = new Vector2(enemy.player.transform.position.x - 6f, enemy.aliveGameObject.transform.position.y);
            }
            else
            {
                enemy.aliveGameObject.transform.position = new Vector2(enemy.player.transform.position.x + 6f, enemy.aliveGameObject.transform.position.y);
            }
            stateMachine.ChangeState(enemy.playerDetectedState);
            entity.isImmortal = false;
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
