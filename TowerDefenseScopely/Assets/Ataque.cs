using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ataque : MonoBehaviour
{
    public SeleccionAtacante seleccion;
    //public EnemyData_SO enemigo;
    //public Enemy enemy;

    private float PosicionGameobject;
    public float ataque;

    public float TiempoCooldown = 2;
    private bool PuedeAtacar = true;
    private bool IniciarCooldown = false;

    [Header("Lista donde se guardan los enemigos para atacarlos")]
    public List<GameObject> enemigos;

    [Header("Lista de enemigos")]
    public List<GameObject> enemigoGameObject;

    [Header("Lista de scripts enemigos")]
    public List<Enemy> enemy;
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
                        
                        if(enemigoGameObject[0].name == enemigos[i].name)
                        {
                            Esbirros es = enemigos[i].GetComponent<Esbirros>();
                            es.TakeDamage(ataque, es.enemyData.damageType);
                        }

                        if (enemigoGameObject[1].name == enemigos[i].name)
                        {
                            Montapuercos es = enemigos[i].GetComponent<Montapuercos>();
                            es.TakeDamage(ataque, es.enemyData.damageType);
                        }

                        if (enemigoGameObject[2].name == enemigos[i].name)
                        {
                            Tanque es = enemigos[i].GetComponent<Tanque>();
                            es.TakeDamage(ataque, es.enemyData.damageType);
                        }

                        if (enemigoGameObject[3].name == enemigos[i].name)
                        {
                            BomberEnemy es = enemigos[i].GetComponent<BomberEnemy>();
                            es.TakeDamage(ataque, es.enemyData.damageType);
                        }

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
            Debug.Log("Ayuda");
            IniciarCooldown = true;
            enemigos.Add(collision.gameObject);
        }
    }


}
