using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public EnemyData_SO enemyData;

    protected Rigidbody2D rb;
    protected Transform currentTarget;
    protected float currentHealth;

    protected float baseSpeed;
    public float currentSpeed { get; protected set; }

    private int slowersCount = 0;
    private Vector2 initialDirection;

    [Header("Detección Torre / Torretas")]
    public float detectionRange = 20f;
    private Transform mainTower;
    private bool chasingTower = false;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.maxHealth;
        baseSpeed = enemyData.moveSpeed;
        currentSpeed = baseSpeed;

        initialDirection = Vector2.right;

        GameObject tower = GameObject.FindGameObjectWithTag("MainTower");
        if (tower != null)
            mainTower = tower.transform;
    }

    protected void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Si no hay torre ni torretas, sigue recto
        if (mainTower == null)
        {
            rb.velocity = initialDirection * currentSpeed;
            return;
        }

        // 🔍 Buscar torreta más cercana (si existe alguna)
        Transform nearestTurret = FindNearestTurret();

        // Si hay una torreta dentro del rango, la tomamos como objetivo
        if (nearestTurret != null)
        {
            currentTarget = nearestTurret;
        }
        else
        {
            // Si no hay torretas cercanas, la torre principal es el objetivo
            currentTarget = mainTower;
        }

        // Calcula la distancia al objetivo actual
        float distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);

        // ✅ Si está dentro del rango, moverse hacia el objetivo
        if (distanceToTarget <= detectionRange)
        {
            MoveTowardsTarget(currentTarget.position);
            chasingTower = true;
        }
        else
        {
            // ✅ Si está fuera del rango: seguir recto
            rb.velocity = initialDirection * currentSpeed;
            chasingTower = false;
        }
    }

    // 🔍 Función para encontrar la torreta más cercana
    private Transform FindNearestTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        Transform nearest = null;
        float nearestDistance = Mathf.Infinity;

        foreach (GameObject turret in turrets)
        {
            float dist = Vector2.Distance(transform.position, turret.transform.position);
            if (dist < nearestDistance && dist <= detectionRange)
            {
                nearestDistance = dist;
                nearest = turret.transform;
            }
        }

        return nearest;
    }

    public void MoveTowardsTarget(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * currentSpeed;
    }

    public void TakeDamage(float amount, DamageType damageType)
    {
        if ((damageType == DamageType.Magical && enemyData.immuneToSpells))
        {
            Debug.Log(enemyData.enemyName + " es inmune a " + damageType + "!");
            return;
        }

        float finalDamage = amount;

        if (damageType == DamageType.Physical)
            finalDamage *= (1 - enemyData.physicalResistance);

        if (damageType == DamageType.Magical)
            finalDamage *= (1 - enemyData.magicalResistance);

        currentHealth -= finalDamage;
        Debug.Log(enemyData.enemyName + " recibió " + finalDamage + " de daño " + damageType);

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public IEnumerator ApplySlow(float amount, float duration)
    {
        if (this == null || gameObject == null) yield break;

        if (slowersCount == 0)
            currentSpeed = currentSpeed * amount;
        else
            currentSpeed = currentSpeed * amount;

        slowersCount++;

        yield return new WaitForSecondsRealtime(duration);

        slowersCount = Mathf.Max(0, slowersCount - 1);

        if (slowersCount == 0)
            currentSpeed = baseSpeed;
        else
            currentSpeed = baseSpeed;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
#endif
}
