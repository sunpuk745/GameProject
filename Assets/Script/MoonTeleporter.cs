using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonTeleporter : MonoBehaviour
{
    [SerializeField] private int loadSceneNumber;

    public int GetDestinationSceneNumber()
    {
        return loadSceneNumber;
    }
}
