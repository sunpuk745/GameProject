using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_Move2State : MoveState
{
    private Enemy3 enemy;

    public E3_Move2State(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (Time.time >= enemy.specialAttackPhase2State.startTime + enemy.specialAttackPhase2StateData.specialAttackCooldown)
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

        if (isPlayerInSpecialSkillRange && enemy.canUseSpecialAttack)
        {
            enemy.canUseSpecialAttack = false;
            stateMachine.ChangeState(enemy.specialAttackPhase2State);
        }
        else if (isPlayerInMeleeRange && !enemy.canUseSpecialAttack)
        {
            stateMachine.ChangeState(enemy.meleeAttack1Phase2State);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
