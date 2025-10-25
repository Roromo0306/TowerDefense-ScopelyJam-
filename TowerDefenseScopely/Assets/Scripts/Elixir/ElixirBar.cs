using System.Collections;
using System.Collections.Generic;
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
    public float currentElixir = 0f;
    public float cargaElixir = 1f;

    // Start is called before the first frame update
    void Start()
    {
        elixirSlider.maxValue= maxElixir;
        elixirText.text = currentElixir.ToString();

    }

    // Update is called once per frame
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
        }

        currentElixir = Mathf.Min(currentElixir, maxElixir);
    }

    void UpdateUI()
    {
        elixirSlider.value = currentElixir;
        elixirText.text = Mathf.FloorToInt(currentElixir).ToString();
      
    }

    public bool HasEnoughElixir(float cost)
    {
        return currentElixir <= cost;
    }

    public void SpendElixir(float cost)
    {
        currentElixir -= cargaElixir;

        if(currentElixir < 0)
        {
            currentElixir = 0;
        }
    }
}
