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

    // 🧭 Dirección inicial (línea recta)
    private Vector2 initialDirection;

    [Header("Detección Torre")]
    public float detectionRange = 20f; // Rango en el que el enemigo "ve" la torre
    private Transform mainTower;      // Referencia a la torre del rey
    private bool chasingTower = false; // Si ya está yendo hacia la torre

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = enemyData.maxHealth;
        baseSpeed = enemyData.moveSpeed;
        currentSpeed = baseSpeed;

        // Dirección inicial hacia la derecha (puedes cambiarla si tus enemigos salen de otro lado)
        initialDirection = Vector2.right;

        // Busca la torre
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
        // Si no hay torre, sigue caminando recto
        if (mainTower == null)
        {
            rb.velocity = initialDirection * currentSpeed;
            return;
        }

        // Calcula la distancia hasta la torre
        float distanceToTower = Vector2.Distance(transform.position, mainTower.position);

        // ✅ Si está dentro del rango: moverse hacia la torre
        if (distanceToTower <= detectionRange)
        {
            MoveTowardsTarget(mainTower.position);
            chasingTower=true;
        }
        else
        {
            // ✅ Si está fuera del rango: seguir recto
            rb.velocity = initialDirection * currentSpeed;
            chasingTower = false;
        }
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

    public void OnTurretPlaced(Transform turret)
    {
        if (enemyData.attackTargetType != AttackTargetType.MainTower)
        {
            currentTarget = turret;
        }
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
