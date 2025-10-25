using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaludEnemigo : MonoBehaviour
{
    private EnemyData_SO enemigo;

    public float saludEnemigo;
    public AttackTargetType tipoAtaque;
    public DamageType tipoDa�o;

    void Start()
    {
        Esbirros esbirros = this.GetComponent<Esbirros>();
        enemigo = esbirros.enemyData;

        saludEnemigo = enemigo.maxHealth;
        tipoAtaque = enemigo.attackTargetType;
        tipoDa�o = enemigo.damageType;
    }

    void Update()
    {
        //saludEnemigo = enemigo.currentHealth;
        if (saludEnemigo <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
