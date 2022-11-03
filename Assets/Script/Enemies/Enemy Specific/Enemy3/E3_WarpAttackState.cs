using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_WarpAttackState : MeleeAttackState
{
    private Enemy3 enemy;

    public E3_WarpAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        AudioManager.Instance.PlaySFX("E3Warp");
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
                enemy.aliveGameObject.transform.position = new Vector2(enemy.player.transform.position.x + 2f, enemy.aliveGameObject.transform.position.y);
            }
            else
            {
                enemy.aliveGameObject.transform.position = new Vector2(enemy.player.transform.position.x - 2f, enemy.aliveGameObject.transform.position.y);
            }
            
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
