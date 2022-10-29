using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void PlaySoudEffect()
    {
        audioSource.Play();
    }
}
