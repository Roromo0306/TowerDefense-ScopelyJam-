using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadaEnemigos : MonoBehaviour
{

    public bool Para = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Para = true;
        }
    }
}
