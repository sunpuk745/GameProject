using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]

public class Data_MeleeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public Color gizmoSpecialAttackColor = Color.cyan;
    public Vector2 specialAttackRange = Vector2.one;
    public Vector2 specialAttackRangeOffset = Vector2.zero;
    public float specialAttackCooldown = 15f;

    public LayerMask whatIsPlayer;
}
