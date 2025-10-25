using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [Header("Spell Prefabs")]
    public GameObject lightningPrefab;
    public GameObject freezePrefab;

    [Header("References")]
    public ElixirBar elixirBar;

    void Update()
    {
        // 🖱️ Clic izquierdo = Lightning
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryCastSpell(lightningPrefab);
        }

        // 🖱️ Clic derecho = Freeze
        if (Input.GetKeyDown(KeyCode.W))
        {
            TryCastSpell(freezePrefab);
        }
    }

    void TryCastSpell(GameObject spellPrefab)
    {
        if (spellPrefab == null) return;

        Spell spell = spellPrefab.GetComponent<Spell>();
        if (spell == null)
        {
            Debug.LogWarning("El prefab no tiene componente Spell.");
            return;
        }

        float cost = spell.elixirCost;

        if (!elixirBar.HasEnoughElixir(cost))
        {
            Debug.Log("No hay suficiente elixir para lanzar " + spellPrefab.name);
            return;
        }

        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; 

        GameObject spellGO = Instantiate(spellPrefab, mousePos, Quaternion.identity);

        // Activar el hechizo
        Spell spellInstance = spellGO.GetComponent<Spell>();
        if (spellInstance != null)
        {
            spellInstance.Cast();
        }

        
        elixirBar.SpendElixir(cost);
    }
}
