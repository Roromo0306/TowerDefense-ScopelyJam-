using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{

    public EnemyData_SO enemyData;
    protected Rigidbody2D rb;
    protected Transform currentTarget;
    protected float currentHealth;
    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.maxHealth;
        FindInitialTarget();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    protected void MoveTowardsTarget()
    {
        if(currentTarget == null)
        {
            return;
        }

        Vector2 direction =(currentTarget.position - transform.position).normalized;
        rb.velocity = direction * enemyData.moveSpeed;
    }
    protected void FindInitialTarget()
    {
        GameObject tower = GameObject.FindGameObjectWithTag("MainTower");
        if (tower != null)
        {
            currentTarget = tower.transform;
        }
    }

    public void TakeDamage(float amount, DamageType damageType)
    {
        float finalDamage = amount;

        if(damageType == DamageType.Physical)
        {
            finalDamage *= (1- enemyData.physicalResistance);
        }

        if(damageType == DamageType.Magical)
        {
            finalDamage *= (1 - enemyData.magicalResistance);
        }

        currentHealth -= finalDamage;

        if(currentHealth <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnTurretPlaced(Transform turret)
    {
        if(enemyData.attackTargetType != AttackTargetType.MainTower)
        {
            currentTarget = turret;
        }
    }
}
