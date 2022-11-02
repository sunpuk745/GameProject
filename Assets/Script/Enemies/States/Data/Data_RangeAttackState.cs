using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]

public class Data_RangeAttackState : ScriptableObject
{
    public GameObject Magic;
    public float MagicDamage;
    public float magicCooldown = 5f;

    public float idleTimeBeforeChangeState = 2f;

    public float projectileDamage = 20f;
    public float projectileSpeed = 12f;
    public float projectileTravelDistance = 10f;
    
    public Vector3 magicSpawnOffset;
}
