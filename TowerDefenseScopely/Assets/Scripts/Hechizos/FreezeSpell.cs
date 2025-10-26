using UnityEngine;
using System.Collections;

public class FreezeSpell : Spell
{
    public float slowAmount = 0.5f;   // multiplicador (0.5 = 50% speed)
    public float freezeTime = 0.5f;
    public Animator animator;
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

                // Ralentizar
                enemy.StartCoroutine(enemy.ApplySlow(slowAmount, freezeTime));

                // Efecto visual azul
                enemy.StartCoroutine(enemy.FlashFreeze(freezeTime));
            }
        }
        animator.SetTrigger("hielo");
        Destroy(gameObject, freezeTime);
    }
}
