using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public SeleccionAtacante seleccion;
    public EnemyData_SO enemigo;

    private float PosicionGameobject;
    public float ataque;

    public List<GameObject> enemigos;
    void Start()
    {
        ataque = seleccion.ataque;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemi = enemigo.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            // enemi.TakeDamage(ataque, enemigo.damageType);
            Debug.Log("hh");
        }
    }


}
