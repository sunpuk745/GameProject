using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundEffect : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private SoAudioClips FootstepsAudioClips_1;
    [SerializeField] private SoAudioClips FootstepsAudioClips_2;
    [SerializeField] private float TimeBeforefadeIn;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;

    void FootStepPlay_1()
    {
        audioSource.PlayOneShot(FootstepsAudioClips_1.GetAudioClip());
    }

    void FootStepPlay_2()
    {
        audioSource.PlayOneShot(FootstepsAudioClips_2.GetAudioClip());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(TimeBeforefadeIn);
        audioSource.DOFade(0.08f, fadeInDuration);
    }

    public void FadeOut()
    {
        audioSource.DOFade(0f, fadeOutDuration);
    }
}
