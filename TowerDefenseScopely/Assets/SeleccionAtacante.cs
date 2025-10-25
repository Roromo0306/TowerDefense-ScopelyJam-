using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionAtacante : MonoBehaviour
{
    [Header("Seleccion Atacante")]
    public TipoAtacante atacante;
    public BaseDatosAtacantes baseDatos;

    [Header("Seleccion de caracteristicas")]
    public TipoAtaque tipoataque;
    public LugarAtaque lugarataque;

    [Header("Stats")]
    public float salud;
    public float ataque;
    public float velocidad;

    private ClaseAtacantes atacantes;

    private void Awake()
    {
        AplicarDatos(atacante);
    }
    
    private void AplicarDatos(TipoAtacante atacante)
    {
        atacantes = baseDatos.GetByType(atacante);

        salud = atacantes.salud;
        ataque = atacantes.ataque;
        velocidad = atacantes.velocidad;

        tipoataque = atacantes.tipoataque;
        lugarataque = atacantes.lugarataque;
    }
}
