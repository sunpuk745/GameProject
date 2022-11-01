using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;
    public DeadState deadState;
    public ChangePhaseState changePhaseState;

    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }

    private void FinishDead()
    {
        deadState.FinishDead();
    }

    private void FinishChangePhaseAnimation()
    {
        changePhaseState.FinishChangePhaseAnimation();
    }
}
