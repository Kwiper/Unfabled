using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeeController : MonoBehaviour
{
    public float idleTime;
    public float speed;
    public float honeInDistance;
    public float moveDistance;
    private EnemyManager enemyManager;

    private float initialY;

    private float randIdleTime;

    [SerializeField] List<Sprite> walkingSprites;

    SpriteAnimator walkAnim;

    [SerializeField] SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        initialY = transform.position.y;
        walkAnim = new SpriteAnimator(walkingSprites, spriteRenderer, 0.16f);
    }

    // Update is called once per frame
    void Update()
    {

        walkAnim.HandleUpdate();
        EnemyState state = enemyManager.getState();
        if(state == EnemyState.Idle){

            bool isInTargetRange = Mathf.Abs(enemyManager.target.transform.position.x - transform.position.x) <= honeInDistance;

            if(!isInTargetRange && enemyManager.timeSinceLastChange > randIdleTime){
                float newX = moveDistance * Random.Range(-1f, 0f) + transform.position.x;
                float newY = moveDistance * Random.Range(-0.4f, 0.4f) + initialY;
                
                Vector2 newPos = new Vector2(newX, newY);

                float dist = Vector2.Distance(newPos, transform.position);
                enemyManager.applyJump(new Vector2(newX, newY), dist/speed);

            }else if(isInTargetRange && enemyManager.timeSinceLastChange > 1){
                Vector2 newPos = enemyManager.target.transform.position;

                float dist = Vector2.Distance(newPos, transform.position);
                enemyManager.applyJump(newPos, 0.5f * dist/speed);

            }

        } else if (state != EnemyState.Jump && state != EnemyState.Death){
            randIdleTime = Random.Range(0f, idleTime);
            enemyManager.setState(EnemyState.Idle);
        }
    }
}
