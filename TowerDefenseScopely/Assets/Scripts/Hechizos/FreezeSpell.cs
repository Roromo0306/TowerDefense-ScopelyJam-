using UnityEngine;
using System.Collections;

public class FreezeSpell : Spell
{
    public float slowAmount = 0.5f;   // multiplicador (0.5 = 50% speed)
    public float freezeTime = 3f;

    protected override void OnCast()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.gameObject.name.Contains("Enemy_Monta"))
                    continue;
                // Ejecutamos la coroutine en el enemy para que use WaitForSecondsRealtime y sea seguro
                enemy.StartCoroutine(enemy.ApplySlow(slowAmount, freezeTime));
            }
        }
        Destroy(gameObject, duration);
    }
}
