using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public EnemyData_SO enemyData;

    protected Rigidbody2D rb;
    protected Transform currentTarget;
    protected float currentHealth;
    public Animator anim;
    protected float baseSpeed;
    public float currentSpeed { get; set; }

    private int slowersCount = 0;
    private Vector2 initialDirection;
    private float tiempoMuerte = 1f;

    [Header("Detección Torre / Torretas")]
    public float detectionRange = 20f;
    private Transform mainTower;
    private bool chasingTower = false;

    // Estado
    private bool isDead = false;

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
        if (isDead) return; // No mover si está muerto
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (mainTower == null)
        {
            rb.velocity = initialDirection * currentSpeed;
            return;
        }

        Transform nearestTurret = FindNearestTurret();

        if (nearestTurret != null)
        {
            currentTarget = nearestTurret;
        }
        else
        {
            currentTarget = mainTower;
        }

        float distanceToTarget = Vector2.Distance(transform.position, currentTarget.position);

        if (distanceToTarget <= detectionRange)
        {
            MoveTowardsTarget(currentTarget.position);
            chasingTower = true;
        }
        else
        {
            rb.velocity = initialDirection * currentSpeed;
            chasingTower = false;
        }
    }

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
        if (isDead) return;
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * currentSpeed;
    }

    public void TakeDamage(float amount, DamageType damageType)
    {
        if (isDead) return; // ignorar si ya está muerto

        if (damageType == DamageType.Magical && enemyData.immuneToSpells)
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
        Debug.Log(enemyData.enemyName + " recibió " + finalDamage + " de daño " + damageType + ". Vida restante: " + currentHealth);

        if (currentHealth <= 0f)
        {
            // Marca el estado muerto
            isDead = true;

            // Para movimiento
            if (rb != null) rb.velocity = Vector2.zero;

            // Desactiva colisiones para que no siga recibiendo interacciones
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            // Lanza la animación de muerte si existe
            if (anim != null)
            {
                anim.SetTrigger("muerte");
                Debug.Log("Estoy triggering la muerte");
            }
            else
            {
                Debug.LogWarning("Animator no asignado en " + gameObject.name);
            }

            // Destruye el objeto tras tiempoMuerte (asegúrate que timeMuerte >= duracion animación)
            Die();
        }
    }

    public void Die()
    {
        // Puedes agregar efectos aquí (partículas, sonido...)
        Destroy(gameObject, tiempoMuerte);
    }

    public IEnumerator ApplySlow(float amount, float duration)
    {
        if (this == null || gameObject == null) yield break;

        slowersCount++;

        // Aplica solo la reducción relativa al baseSpeed — ejemplo: amount = 0.5 = 50% velocidad
        currentSpeed = baseSpeed * amount;

        yield return new WaitForSecondsRealtime(duration);

        slowersCount = Mathf.Max(0, slowersCount - 1);

        if (slowersCount == 0)
            currentSpeed = baseSpeed;
        else
            currentSpeed = baseSpeed * 1f; // o calcula acumulación si la quieres
    }

    public IEnumerator FlashFreeze(float duration)
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if (rend == null) yield break;

        Color original = rend.color;
        Color flashColor = Color.cyan;

        rend.color = flashColor;
        yield return new WaitForSeconds(duration);

        if (rend != null)
            rend.color = original;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
#endif
}
