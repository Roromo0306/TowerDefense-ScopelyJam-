using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public SeleccionAtacante seleccion;
    public EnemyData_SO enemigo;
    public Enemy enemy;

    private float PosicionGameobject;
    public float ataque;

    public float TiempoCooldown = 2;
    private bool PuedeAtacar = true;
    private bool IniciarCooldown = false;

    public List<GameObject> enemigos;
    void Start()
    {
        ataque = seleccion.ataque;
    }

    private void Update()
    {
        if (IniciarCooldown)
        {
            TiempoCooldown -= Time.deltaTime;
        }

        if(TiempoCooldown <= 0)
        {
            PuedeAtacar = true;
            TiempoCooldown = 2;
        }


        if(enemigos.Count != 0)
        {
            if (PuedeAtacar)
            {
                for (int i = 0; i < enemigos.Count; i++)
                {
                    if (enemigos[i] != null)
                    {

                        Esbirros es = enemigos[i].GetComponent<Esbirros>();

                        
                        es.TakeDamage(ataque, es.enemyData.damageType);
                    }
                }
                PuedeAtacar = false;

                enemigos.RemoveAll(e => e == null);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            IniciarCooldown = true;
            enemigos.Add(collision.gameObject);
        }
    }


}
