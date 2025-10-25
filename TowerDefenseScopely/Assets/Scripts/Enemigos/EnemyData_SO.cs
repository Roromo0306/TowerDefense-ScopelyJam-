using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy")]
public class EnemyData_SO : ScriptableObject
{
    [Header("Info")]
    public string enemyName;
    public EnemyTipe enemyTipe;
    public AttackTargetType attackTargetType;
    public DamageType damageType;


    [Header("Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 2f;
    public float attackDamage = 10f;
    public float attackRange = 1.5f;
    public float attackRate = 1f;

    [Header("Resistencias")]
    [Range(0f,1f)] public float physicalResistance = 0.5f;
    [Range(0f,1f)] public float magicalResistance = 0.5f;

}
