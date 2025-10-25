using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaRey : MonoBehaviour
{
    public float Salud;

    private SeleccionAtacante seleccion;
    void Start()
    {
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
    }

    void Update()
    {
        
    }
}
