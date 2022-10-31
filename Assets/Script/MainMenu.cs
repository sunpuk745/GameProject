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

    public void PlayGame()
    {
        menuAudioSource.PlayOneShot(selected_AudioClip.GetAudioClip());
        title_transition.SetTrigger("Start");
        levelLoader.LoadNextLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
