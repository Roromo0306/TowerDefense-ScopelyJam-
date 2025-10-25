using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Atacante/TipoAtacante", fileName = "NuevaClaseAtacante")]
public class ClaseAtacantes : ScriptableObject
{
    public TipoAtacante atacante;
    public TipoAtaque tipoataque;
    public LugarAtaque lugarataque;

    public float salud = 0;
    public float ataque = 0;
    public float velocidad = 0;
}
