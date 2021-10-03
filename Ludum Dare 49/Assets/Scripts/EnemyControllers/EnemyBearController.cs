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
    
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = enemyManager.target.transform.position;
        Vector3 currentPos = transform.position;
        Vector3 posDiff = targetPos - currentPos;


        if(Mathf.Abs(posDiff.x) < jumpDistance){
            EnemyState state = enemyManager.getState();

            if(state == EnemyState.Moving){
                enemyManager.setState(EnemyState.Idle);
            }else if(state == EnemyState.Idle && enemyManager.timeSinceLastChange > idleTime){
                enemyManager.applyJump((Vector2) targetPos, jumpTime);
            }
        }
    }
}
