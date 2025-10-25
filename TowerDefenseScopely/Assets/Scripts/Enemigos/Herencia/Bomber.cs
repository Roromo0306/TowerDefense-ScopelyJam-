using UnityEngine;

public class BomberEnemy : Enemy
{
    [Header("Bomb Settings")]
    public GameObject bombPrefab;
    public float attackRange = 5f;      // rango para detectar torretas
    public float throwInterval = 2f;    // tiempo entre lanzamientos
    public float bombFlightTime = 1f;   // duración del vuelo

    private float throwTimer;

    protected void FixedUpdate()
    {
        base.FixedUpdate(); // mantiene movimiento normal hacia la torre

        throwTimer += Time.deltaTime;

        // Buscar torretas solo si este enemigo tiene permitido atacarlas
        if (enemyData.attackTargetType == AttackTargetType.Both ||
            enemyData.attackTargetType == AttackTargetType.Turrets)
        {
            Transform turret = FindNearestTurret();

            if (turret != null)
            {
                float distance = Vector2.Distance(transform.position, turret.position);

                if (distance <= attackRange)
                {
                    rb.velocity = Vector2.zero; // se detiene antes de lanzar

                    if (throwTimer >= throwInterval)
                    {
                        ThrowBomb(turret.position);
                        throwTimer = 0f;
                    }
                }
            }
        }
    }

    void ThrowBomb(Vector2 targetPos)
    {
        if (bombPrefab == null) return;

        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        BombProjectile projectile = bomb.GetComponent<BombProjectile>();

        if (projectile != null)
            projectile.Launch(transform.position, targetPos, bombFlightTime);
    }

    Transform FindNearestTurret()
    {
        
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        Transform closest = null;
        float minDist = Mathf.Infinity;

        foreach (GameObject t in turrets)
        {
            float dist = Vector2.Distance(transform.position, t.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = t.transform;
            }
        }

        return closest;
    }
}

