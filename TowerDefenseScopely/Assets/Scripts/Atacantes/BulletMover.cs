using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 8f;

    public SpriteRenderer rend;

    public float flashDuration = 0.5f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            if (collision.TryGetComponent<SpriteRenderer>(out SpriteRenderer rend))
            {
                StartCoroutine(FlashAndDestroy(rend, Color.red, flashDuration));
            }
        }
    }

    private IEnumerator FlashAndDestroy(SpriteRenderer rend, Color flashColor, float duration)
    {

        Color original = rend.color;
        rend.color = flashColor;

        yield return new WaitForSeconds(duration);

 
        if (rend != null)
        {
            rend.color = original;
        }

        Destroy(this.gameObject);
    }
    }

