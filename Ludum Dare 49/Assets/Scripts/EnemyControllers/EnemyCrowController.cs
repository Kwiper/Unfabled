using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowController : MonoBehaviour
{
    public float moveTime;
    public float honeInDistance;
    private EnemyManager enemyManager;

    // Sprites
    [SerializeField] List<Sprite> flyingSprites;
    [SerializeField] List<Sprite> attackingSprites;

    SpriteAnimator flyAnim;
    SpriteAnimator attackAnim;
    SpriteAnimator currentAnim;

    [SerializeField] SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();

        flyAnim = new SpriteAnimator(flyingSprites, spriteRenderer, 0.16f);
        attackAnim = new SpriteAnimator(attackingSprites, spriteRenderer, 0.16f);

        currentAnim = flyAnim;
    }

    // Update is called once per frame
    void Update()
    {
        currentAnim.HandleUpdate();
        
        if(enemyManager.getState() == EnemyState.Moving){
            if(enemyManager.timeSinceLastChange > moveTime){
                currentAnim = attackAnim;
                bool isInTargetRange = Mathf.Abs(enemyManager.target.transform.position.x - transform.position.x) <= honeInDistance;

                Vector2 jumpLocation = isInTargetRange ? 
                    (Vector2)enemyManager.target.transform.position : 
                    (Vector2)transform.position + new Vector2(-2, 0);
                
                
                GetComponent<Rigidbody2D>().gravityScale = -0.4f;
                enemyManager.applyJump(jumpLocation, 2f);
            }else{
                GetComponent<Rigidbody2D>().gravityScale = 0;
                currentAnim = flyAnim;
            }
        }
    }
}
