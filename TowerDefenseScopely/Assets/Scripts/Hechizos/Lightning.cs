using UnityEngine;

public class Lightning : Spell
{
    public float damage = 50f;

    protected override void Start()
    {
        base.Start();
        AffectEnemies();
    }

    protected override void ApplyEffect(Collider2D[] targets)
    {
        foreach (Collider2D hit in targets)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, DamageType.Magical); // Usas tu enum de daño
            }
        }
    }
}
