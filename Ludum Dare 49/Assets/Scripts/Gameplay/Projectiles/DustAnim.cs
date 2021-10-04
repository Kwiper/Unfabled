using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustAnim : MonoBehaviour
{

    [SerializeField] List<Sprite> dustSprites;

    SpriteAnimator anim;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        anim = new SpriteAnimator(dustSprites, spriteRenderer);
    }

    // Update is called once per frame
    void Update()
    {
        anim.HandleUpdate();
    }
}
