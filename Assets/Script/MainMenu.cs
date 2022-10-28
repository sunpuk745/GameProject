using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{

    public AudioSource menuAudioSource;
    [SerializeField] private SoAudioClips selected_AudioClip;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Animator title_transition;

    private void Awake()
    {
        StartCoroutine(musicFadeIn());
    }

    public void PlayGame()
    {
        Selected_SoundEffectPlay();
        StartCoroutine(musicFadeOut());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void Selected_SoundEffectPlay()
    {
        menuAudioSource.PlayOneShot(selected_AudioClip.GetAudioClip());
    }

    private IEnumerator musicFadeIn()
    {
        yield return new WaitForSeconds(1.5f);
        menuAudioSource.Play();
        menuAudioSource.DOFade(0.2f, 10f);
    }

    private IEnumerator musicFadeOut()
    {
        title_transition.SetTrigger("Start");
        menuAudioSource.DOFade(0f, 1f);
        levelLoader.LoadNextLevel();
        yield return new WaitForSeconds(0.1f);
        DOTween.KillAll();
    }

}
