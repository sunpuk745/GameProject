using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    public AudioSource bgm;
    public Animator transition;
    public float waitBeforeLoadScene = 1f;
    public float waitBeforePlaySound = 1.5f;
    
    [SerializeField] private float fadeInSoundVolume;
    [SerializeField] private float fadeInSoundDuration;
    [Space(10)]
    [SerializeField] private float fadeOutSoundVolume;
    [SerializeField] private float fadeOutSoundDuration;

    private void Awake()
    {
        StartCoroutine(musicFadeIn());
    }

    public void LoadNextLevel()
    {
        StartCoroutine(musicFadeOut());
        DOTween.KillAll();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(waitBeforeLoadScene);
        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator musicFadeIn()
    {
        yield return new WaitForSeconds(waitBeforePlaySound);
        bgm.Play();
        bgm.DOFade(fadeInSoundVolume, fadeInSoundDuration);
    }

    private IEnumerator musicFadeOut()
    {
        bgm.DOFade(fadeOutSoundVolume, fadeOutSoundDuration);
        yield return new WaitForSeconds(fadeOutSoundDuration + 0.5f);
        DOTween.KillAll();
    }

}
