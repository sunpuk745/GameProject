using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{

    public AudioSource menuAudioSource;
    public GameObject howToPlayUI;

    [SerializeField] private SoAudioClips selected_AudioClip;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private Animator title_transition;

    public void PlayGame()
    {
        menuAudioSource.PlayOneShot(selected_AudioClip.GetAudioClip());
        title_transition.SetTrigger("Start");
        levelLoader.LoadNextLevel();
    }

    public void ExitGame()
    {
        PlaySound();
        Application.Quit();
    }

    public void PlaySound()
    {
        menuAudioSource.PlayOneShot(selected_AudioClip.GetAudioClip());
    }

    public void OpenHowToPlay()
    {
        PlaySound();
        howToPlayUI.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        PlaySound();
        howToPlayUI.SetActive(false);
    }

    public void DeveloperAdvantage()
    {
        PlaySound();
        SceneManager.LoadScene(8);
    }

    public void DeveloperAdvantage2()
    {
        PlaySound();
        SceneManager.LoadScene(0);
    }
}
