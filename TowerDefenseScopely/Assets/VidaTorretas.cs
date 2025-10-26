using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    public float CoolDown = 2f;
    public bool IniciarCooldown = false;
    public bool PuedeAtacar = true;
    void Start()
    {
        Time.timeScale = 1;
        seleccion = GetComponent<SeleccionAtacante>();
        Salud = seleccion.salud;
        SaludMax = seleccion.salud;
        
    }

    void Update()
    {

        Debug.Log("La vida de la torre es " + Salud);
        barraVida.fillAmount = Mathf.Clamp01(Salud / SaludMax);

        if(CoolDown <= 0)
        {
            Debug.Log("Cooldown cero");
            PuedeAtacar = true;
            CoolDown = 2f;
        }
        else
        {
            CoolDown -= Time.deltaTime;
            Debug.Log("El cooldown es "+CoolDown);
        }

        if (Salud <= 0)
        {
            Debug.Log("Salud es cero");
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
                Debug.Log("Colision enemigo");
                EnemigosColisionado.Add(collision.gameObject);
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    previousVelocity = rb.velocity;
                    previousConstraints = rb.constraints;

                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    // PuedeAtacar = false;

                }
                
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PuedeAtacar)
        {
            Salud -= enemigo.attackDamage;
            PuedeAtacar = false;

            Debug.Log("Ataca");
            
        }
    }
}
