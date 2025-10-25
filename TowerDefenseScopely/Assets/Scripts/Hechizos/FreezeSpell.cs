using UnityEngine;
using System.Collections;

public class FreezeSpell : Spell
{
    public float slowAmount = 0.5f;
    public float freezeTime = 3f;

    protected override void OnCast()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                StartCoroutine(FreezeEnemy(enemy));
            }
        }
        Destroy(gameObject, duration);
    }

    private IEnumerator FreezeEnemy(Enemy enemy)
    {
        float originalSpeed = enemy.enemyData.moveSpeed;
        enemy.enemyData.moveSpeed *= slowAmount;
        yield return new WaitForSeconds(freezeTime);
        if (enemy != null)
            enemy.enemyData.moveSpeed = originalSpeed;
    }
}

