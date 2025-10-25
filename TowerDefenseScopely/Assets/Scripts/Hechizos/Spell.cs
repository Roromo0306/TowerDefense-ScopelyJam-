using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [Header("Base Settings")]
    public float radius = 3f;
    public float duration = 1f;
    public float elixirCost = 3f;

    public void Cast()
    {
        OnCast();
        Destroy(gameObject, duration);
    }

    protected abstract void OnCast();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
