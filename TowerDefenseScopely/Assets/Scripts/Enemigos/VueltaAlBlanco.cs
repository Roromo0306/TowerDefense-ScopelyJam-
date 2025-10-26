using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VueltaAlBlanco : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float curTime = float.MaxValue;

    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime >= 4f)
        {
            curTime = 0f;
            BlancoCore();
        }
    }

    void BlancoCore()
    {
        spriteRenderer.color = Color.white;
        Debug.Log("En blanco");
    }
}
