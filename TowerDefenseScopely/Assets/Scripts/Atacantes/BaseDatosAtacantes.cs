using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Atacante/BaseDatos", fileName = "NuevaBaseDatosAtacante")]
public class BaseDatosAtacantes : ScriptableObject
{
    public List<ClaseAtacantes> clase = new();

    public ClaseAtacantes GetByType(TipoAtacante tipoAtacante)
    {
        for (int i = 0; i < clase.Count; i++)
        {
            if (clase[i].atacante == tipoAtacante)
            {
                return clase[i];
            }
        }

        return null;
    }
}
