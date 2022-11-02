using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_SpecialAttackState : MeleeAttackState
{
    private Enemy3 enemy;

    public E3_SpecialAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            enemy.canUseSpecialAttack = false;
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedPlayer = Physics2D.OverlapBoxAll((Vector2)attackPosition.position + stateData.specialAttackRangeOffset, stateData.specialAttackRange, 0, stateData.whatIsPlayer);

        foreach (Collider2D collider2 in detectedPlayer)
        {
            collider2.transform.SendMessage("Damage", attackDetails);
        }
    }
}
