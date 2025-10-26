using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Time.timeScale = 1;
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
        panelHasPerdido.SetActive(false);
    }

    bool finished = false;

    void Update()
    {
        barraVida.fillAmount = Mathf.Clamp01(Salud/ SaludMax);

        if(Salud <= 0 && !finished)
        {

            finished = true;            
            panelHasPerdido.SetActive(true);
            Time.timeScale = 0;
            //Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Debug.Log("Rigidbody");
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
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
