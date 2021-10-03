using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuardController : MonoBehaviour
{

    [SerializeField] List<Sprite> walkingSprites;
    SpriteAnimator walkAnim;

    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        walkAnim = new SpriteAnimator(walkingSprites, spriteRenderer, 0.16f);
    }

    // Update is called once per frame
    void Update()
    {
        walkAnim.HandleUpdate();
    }
}
