using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFleeStateData", menuName = "Data/State Data/Flee State")]

public class Data_FleeState : ScriptableObject
{
    public float fleeTime = 1.5f;
    public float fleeTimeCooldown = 3f;
    public float fleeSpeed = 6f;

    public float teleportDistance = 10f;
}
