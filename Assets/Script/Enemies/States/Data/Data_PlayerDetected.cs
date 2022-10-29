using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]

public class Data_PlayerDetected : ScriptableObject
{
    public float longRangeActionTime = 1.5f;
    public float magicCastTime = 3f;

    public float distanceToPlayer;
}
