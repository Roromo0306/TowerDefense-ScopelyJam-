using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidaRey : MonoBehaviour
{
    public float Salud;
    public EnemyData_SO enemigo;
    public Image barraVida;

    private SeleccionAtacante seleccion;
    private float SaludMax;
    void Start()
    {
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
    }

    void Update()
    {
        barraVida.fillAmount = Mathf.Clamp01(Salud/ SaludMax);

        if(Salud <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Salud -= enemigo.attackDamage;
        }
    }
}
