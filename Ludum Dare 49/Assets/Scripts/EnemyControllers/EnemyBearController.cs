using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBearController : MonoBehaviour
{
    // Start is called before the first frame update

    public float idleTime;
    public float jumpTime;
    public float jumpDistance;
    private EnemyManager enemyManager;

    // Sprites
    [SerializeField] List<Sprite> walkingSprites;
    [SerializeField] List<Sprite> jumpingSprites;

    SpriteAnimator walkAnim;
    SpriteAnimator jumpAnim;
    SpriteAnimator currentAnim;
    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        walkAnim = new SpriteAnimator(walkingSprites, spriteRenderer, 0.16f);
        jumpAnim = new SpriteAnimator(jumpingSprites, spriteRenderer, 0.16f);

        currentAnim = walkAnim;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = enemyManager.target.transform.position;
        Vector3 currentPos = transform.position;
        Vector3 posDiff = targetPos - currentPos;

        currentAnim.HandleUpdate();

        if (Mathf.Abs(posDiff.x) < jumpDistance)
        {
            EnemyState state = enemyManager.getState();

            if (state == EnemyState.Moving)
            {
                enemyManager.setState(EnemyState.Idle);
                currentAnim = walkAnim;
            }
            else if (state == EnemyState.Idle && enemyManager.timeSinceLastChange > idleTime)
            {
                enemyManager.applyJump((Vector2)targetPos, jumpTime);
                currentAnim = jumpAnim;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "BearKiller") Destroy(gameObject);
    }
}
