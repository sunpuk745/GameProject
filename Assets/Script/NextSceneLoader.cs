using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class NextSceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
