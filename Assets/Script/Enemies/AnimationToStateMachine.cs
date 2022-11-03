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

    private void PlayE3Move1()
    {
        AudioManager.Instance.PlaySFX("E3Move1");
    }

    private void PlayE3Special()
    {
        AudioManager.Instance.PlaySFX("E3Special");
    }

    private void PlayE3Charge()
    {
        AudioManager.Instance.PlaySFX("E3Charge");
    }

    private void PlayE3Putdown()
    {
        AudioManager.Instance.PlaySFX("E3Putdown");
    }
}
