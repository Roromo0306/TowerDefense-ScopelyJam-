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

    public GameObject panelHasPerdido;

    private SeleccionAtacante seleccion;
    private float SaludMax;
    void Start()
    {
        Time.timeScale = 1;
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
        panelHasPerdido.SetActive(false);
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

    public void Reintentar()
    {

        SceneManager.LoadScene("Juego");
        Time.timeScale = 1;
        Debug.Log("timescale a 1");
        finished = false;

    }

    public void Salir()
    {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}
