using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaTorretas : MonoBehaviour
{
    public float Salud;
    public EnemyData_SO enemigo;
    public Image barraVida;

    

    private SeleccionAtacante seleccion;
    private float SaludMax;
    void Start()
    {
        Time.timeScale = 1;
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
        
    }

    bool finished = false;

    void Update()
    {
        barraVida.fillAmount = Mathf.Clamp01(Salud / SaludMax);

        if (Salud <= 0)
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
