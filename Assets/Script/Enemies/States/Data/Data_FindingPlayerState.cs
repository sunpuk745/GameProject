using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFindingPlayerStateData", menuName = "Data/State Data/Finding Player State")]

public class Data_FindingPlayerState : ScriptableObject
{
    public int turnAmount = 2;
    public float timeBetweenTurn = 0.75f;
}
