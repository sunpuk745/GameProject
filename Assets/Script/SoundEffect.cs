using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private SoAudioClips FootstepsAudioClips_1;
    [SerializeField] private SoAudioClips FootstepsAudioClips_2;

    void FootStepPlay_1()
    {
        audioSource.PlayOneShot(FootstepsAudioClips_1.GetAudioClip());
    }

    void FootStepPlay_2()
    {
        audioSource.PlayOneShot(FootstepsAudioClips_2.GetAudioClip());
    }

    void FadeIn()
    {
        audioSource.DOFade(0.25f, 6f).SetEase(Ease.OutSine);
    }

    void FadeOut()
    {
        audioSource.DOFade(0f, 3f);
    }
}
