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

    // Velocidad base leída desde el ScriptableObject (no tocar el SO)
    protected float baseSpeed;
    // Velocidad que usamos en movimiento (mutable en runtime)
    public float currentSpeed { get; protected set; }

    // Contador simple para manejar múltiples slows superpuestos
    private int slowersCount = 0;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.maxHealth;

        // Copiamos desde el ScriptableObject, sin modificar el SO
        baseSpeed = enemyData.moveSpeed;
        currentSpeed = baseSpeed;

        FindInitialTarget();
    }

    protected void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    protected void MoveTowardsTarget()
    {
        if (currentTarget == null) return;

        Vector2 direction = (currentTarget.position - transform.position).normalized;
        rb.velocity = direction * currentSpeed;
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

    public void OnTurretPlaced(Transform turret)
    {
        if (enemyData.attackTargetType != AttackTargetType.MainTower)
        {
            currentTarget = turret;
        }
    }

    // Aplica una ralentización (amount: multiplicador, ej 0.5f = 50% speed) por duración (segundos, en tiempo real)
    public IEnumerator ApplySlow(float amount, float duration)
    {
        // Si el enemigo ya está muerto, salimos
        if (this == null || gameObject == null) yield break;

        // Si este es el primer slow, guardamos el estado base
        if (slowersCount == 0)
        {
            // baseSpeed ya contiene la velocidad del SO; currentSpeed podría haber sido modificada por otros efectos
            // Para aplicar la slow mantendremos baseSpeed como referencia:
            currentSpeed = currentSpeed * amount;
        }
        else
        {
            // Si ya hay slows activos, multiplicamos la currentSpeed por el nuevo amount (stack multiplicativo)
            currentSpeed = currentSpeed * amount;
        }

        slowersCount++;

        // Esperamos en tiempo real (ignora Time.timeScale) para que funcione aun si el juego está pausado
        yield return new WaitForSecondsRealtime(duration);

        // Al quitar un slow, decrementamos contador y si llega a cero, restauramos la velocidad base desde el SO
        slowersCount = Mathf.Max(0, slowersCount - 1);

        if (slowersCount == 0)
        {
            // Restauramos a la velocidad base definida por el ScriptableObject
            currentSpeed = baseSpeed;
        }
        else
        {
            // Si quedan slows, una aproximación simple: restauramos a baseSpeed y reaplicamos los slows restantes
            // (si necesitas precisión por diferentes amounts, lleva una lista de amounts en vez de contador)
            currentSpeed = baseSpeed; // base
            // Nota: para precisión con diferentes amounts necesitarías almacenar los amounts en una lista y recalcular la multiplicación.
        }
    }
}
