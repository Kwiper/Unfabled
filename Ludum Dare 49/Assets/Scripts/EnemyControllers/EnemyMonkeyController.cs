using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonkeyController : MonoBehaviour
{
    public float idleTime;
    public float jumpTime;
    public float jumpDistance;
    private EnemyManager enemyManager;

    [SerializeField] List<Sprite> jumpingSprites;
    [SerializeField] List<Sprite> idleSprite;

    SpriteAnimator jumpAnim;
    SpriteAnimator idle;
    SpriteAnimator currentAnim;

    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.setState(EnemyState.Idle);

        jumpAnim = new SpriteAnimator(jumpingSprites, spriteRenderer, 0.16f);
        idle = new SpriteAnimator(idleSprite, spriteRenderer, 0.16f);

        currentAnim = idle;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyState state = enemyManager.getState();
        currentAnim.HandleUpdate();

        if(state == EnemyState.Moving){
            enemyManager.setState(EnemyState.Idle);
            currentAnim = idle;
        }else if(state == EnemyState.Idle && enemyManager.timeSinceLastChange > idleTime){
            currentAnim = jumpAnim;
            enemyManager.applyJump((Vector2) transform.position + new Vector2(-jumpDistance, 0f), jumpTime);
        }
    }
}
