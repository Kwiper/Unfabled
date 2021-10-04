using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    [SerializeField] List<Sprite> flareAnim;

    SpriteAnimator flare;

    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        flare = new SpriteAnimator(flareAnim, spriteRenderer, 0.8f);
    }

    private void Update()
    {
        flare.HandleUpdate();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
            Destroy(gameObject);
        }
    }
}
