using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    public float damage = 20f;
    public float explosionRadius = 1.5f;
    public float gravity = -9.8f;

    private Vector2 startPos;
    private Vector2 targetPos;
    private float flightTime;
    private float elapsed;
    private Vector2 velocity;

    public void Launch(Vector2 start, Vector2 target, float time)
    {
        startPos = start;
        targetPos = target;
        flightTime = time;
        elapsed = 0;

        Vector2 displacement = target - start;
        float vx = displacement.x / time;
        float vy = (displacement.y - 0.5f * gravity * time * time) / time;
        velocity = new Vector2(vx, vy);
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        Vector2 position = startPos + velocity * elapsed + 0.5f * new Vector2(0, gravity) * elapsed * elapsed;
        transform.position = position;

        if (elapsed >= flightTime)
            Explode();
    }

    void Explode()
    {
        // Ejemplo: daño en área
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Turret"))
            {
                // hit.GetComponent<TurretHealth>()?.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

