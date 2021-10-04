using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    SpriteRenderer sprite;
    float alpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.color = new Color(1, 1, 1, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(1, 1, 1, alpha);

        alpha += Time.deltaTime * 0.75f;

        Mathf.Clamp(alpha, 0, 1);

    }
}
