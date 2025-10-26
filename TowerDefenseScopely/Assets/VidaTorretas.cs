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

    public List<GameObject> EnemigosColisionado;
    private RigidbodyConstraints2D previousConstraints;
    private Vector2 previousVelocity;

    private int NumeroLista;
    private bool Deja = false;

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
            Deja = true;
            NumeroLista = EnemigosColisionado.Count;

            for (int i = 0; i < EnemigosColisionado.Count; i++)
            {
                if(EnemigosColisionado[i] != null)
                {
                    Rigidbody2D rb = EnemigosColisionado[i].GetComponent<Rigidbody2D>();

                    rb.constraints = previousConstraints;
                    rb.velocity = previousVelocity;
                }
                
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            if (!Deja)
            {
                EnemigosColisionado.Add(collision.gameObject);
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    previousVelocity = rb.velocity;
                    previousConstraints = rb.constraints;

                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
                Salud -= enemigo.attackDamage;
            }
            
        }
    }
}
