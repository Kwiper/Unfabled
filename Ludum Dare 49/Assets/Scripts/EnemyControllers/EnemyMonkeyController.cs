using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonkeyController : MonoBehaviour
{
    public float idleTime;
    public float jumpTime;
    public float jumpDistance;
    private EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyManager.setState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyState state = enemyManager.getState();

        if(state == EnemyState.Moving){
            enemyManager.setState(EnemyState.Idle);
        }else if(state == EnemyState.Idle && enemyManager.timeSinceLastChange > idleTime){
            enemyManager.applyJump((Vector2) transform.position + new Vector2(-jumpDistance, 0f), jumpTime);
        }
    }
}
