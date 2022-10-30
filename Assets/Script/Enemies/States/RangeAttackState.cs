using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected Data_RangeAttackState stateData;

    protected GameObject Magic;
    protected Magic magicScript;

    protected bool DetectPlayerInRange;

    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        DetectPlayerInRange = entity.DetectPlayerInRange();

        Magic = GameObject.Instantiate(stateData.Magic, attackPosition.position, attackPosition.rotation);
        magicScript =  Magic.GetComponent<Magic>();
        magicScript.FireMagic(stateData.MagicDamage);
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
