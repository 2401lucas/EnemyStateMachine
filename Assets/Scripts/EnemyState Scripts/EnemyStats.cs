using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats : ScriptableObject
{
    public float moveSpeed;
    public float lookRange;
    public float lookSphereCastRadius;
    public float attackRange;
    public float attackRate;
    public float attackForce;
    public float attackDamage;
    public float searchDuration;
    public float searchingTurnSpeed;


}
