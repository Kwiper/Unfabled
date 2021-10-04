using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] List<Sprite> explosion;

    SpriteAnimator explosionAnim;

    [SerializeField] SpriteRenderer spriteRenderer;


    [SerializeField] float duration;
    // Start is called before the first frame update
    void Start()
    {
        explosionAnim = new SpriteAnimator(explosion, spriteRenderer, 0.1f);
        Destroy(gameObject, duration);
    }

    private void Update()
    {
        explosionAnim.HandleUpdate();
    }
}
