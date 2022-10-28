using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]

public class Data_Entity : ScriptableObject
{
    public float maxHealth = 30f;

    public float DamageKnockSpeed = 3f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float groundCheckRadius = 0.3f;

    public float minAggroDistance = 2f;
    public float maxAggroDistance = 4f;
    
    public float stunResistance = 3f;
    public float stunRecoveryTimme = 2f;

    public float closeRangeActionDistance = 1f;

    public LayerMask Ground;
    public LayerMask Player;
}
