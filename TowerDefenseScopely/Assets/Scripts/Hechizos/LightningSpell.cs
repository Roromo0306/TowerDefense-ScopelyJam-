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
                enemy.TakeDamage(damage, DamageType.Magical);
            }
        }
        Destroy(gameObject, 0.2f);
    }
}
