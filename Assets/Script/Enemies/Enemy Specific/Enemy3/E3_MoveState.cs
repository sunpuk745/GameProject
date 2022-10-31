using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    private Enemy3 enemy;

    public E3_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (DetectPlayerInRange)
        {
            entity.SetVelocity(stateData.movementSpeed);
        }

        if (enemy.player.transform.position.x > enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x < 0 
        || enemy.player.transform.position.x < enemy.aliveGameObject.transform.position.x && enemy.aliveGameObject.transform.localScale.x > 0)
        {
            entity.Flip();
        }

        if (isPlayerInMeleeRange && !enemy.canUseSpecialAttack)
        {
            stateMachine.ChangeState(enemy.meleeAttack1State);
        }
        else if (isPlayerInSpecialSkillRange && enemy.canUseSpecialAttack)
        {
            enemy.canUseSpecialAttack = false;
            stateMachine.ChangeState(enemy.specialAttackState);
        }

        if (Time.time >= enemy.specialAttackState.startTime + enemy.specialAttackStateData.specialAttackCooldown)
        {
            enemy.canUseSpecialAttack = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
