using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElixirBar : MonoBehaviour
{
    [Header("UI")]
    public Slider elixirSlider;
    public TextMeshProUGUI elixirText;

    [Header("Elixir Settings")]
    public float maxElixir = 10f;
    [Min(0)]public float currentElixir = 5f;
    public float cargaElixir = 1f; // cantidad regenerada por segundo

    void Start()
    {
        elixirSlider.maxValue = maxElixir;
        elixirSlider.value = currentElixir;
        UpdateUI();
    }

    void Update()
    {
        RegenerarElixir();
        UpdateUI();
    }

    void RegenerarElixir()
    {
        if (currentElixir < maxElixir)
        {
            currentElixir += cargaElixir * Time.deltaTime;
            currentElixir = Mathf.Min(currentElixir, maxElixir);
        }
    }

    void UpdateUI()
    {
        elixirSlider.value = currentElixir;
        elixirText.text = Mathf.FloorToInt(currentElixir).ToString();
    }

    // ✅ CORRECTO: debe ser >= para poder lanzar hechizo
    public bool HasEnoughElixir(float cost)
    {
        return currentElixir >= cost;
    }

    // ✅ CORRECTO: se resta el coste, no la tasa de carga
    public void SpendElixir(float cost)
    {
        currentElixir -= cost;
        currentElixir = Mathf.Max(0, currentElixir);
    }
}

