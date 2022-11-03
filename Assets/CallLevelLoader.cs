using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallLevelLoader : MonoBehaviour
{
    [SerializeField] private LevelLoader loader;

    
    void OnEnable()
    {
        loader.LoadNextLevel();
    }

}
