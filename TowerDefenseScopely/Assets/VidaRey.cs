using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaRey : MonoBehaviour
{
    public float Salud;
    public EnemyData_SO enemigo;
    public Image barraVida;

    public GameObject panelHasPerdido;

    private SeleccionAtacante seleccion;
    private float SaludMax;
    void Start()
    {
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
        panelHasPerdido.SetActive(false);
    }

    void Update()
    {
        barraVida.fillAmount = Mathf.Clamp01(Salud/ SaludMax);

        if(Salud <= 0)
        {
            Destroy(this.gameObject);
            panelHasPerdido.SetActive(true);
            Time.timeScale = 0;
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
        Time.timeScale = 1;
        SceneManager.LoadScene("Juego");
    }

    public void Salir()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}
