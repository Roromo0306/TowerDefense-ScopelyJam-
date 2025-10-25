using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("Base Spell Settings")]
    public float radius = 3f;
    public float duration = 2f;

    protected float timer;

    protected virtual void Start()
    {
        timer = 0f;
    }

    protected  void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            OnSpellEnd();
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect(Collider2D[] targets);

    protected  void OnSpellEnd()
    {
        // Para efectos visuales (humo, partículas, etc.)
    }

    protected void AffectEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        ApplyEffect(hits);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
