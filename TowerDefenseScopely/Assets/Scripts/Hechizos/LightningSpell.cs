using UnityEngine;

public class LightningSpell : Spell
{
    public float damage = 50f;

    protected override void OnCast()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Ignorar Montapuercos
                if (enemy.gameObject.name.Contains("Montapuercos"))
                {
                    Debug.Log("Ignorado Montapuercos");
                    continue;
                }

                Debug.Log("Golpeando a: " + enemy.gameObject.name);
                enemy.TakeDamage(damage, DamageType.Magical);
            }
        }

        Destroy(gameObject, 0.2f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
