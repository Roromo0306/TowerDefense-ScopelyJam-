using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 8f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void Start()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Destroy(this.gameObject);
        }
    }
}
