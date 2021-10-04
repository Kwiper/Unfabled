using Action = System.Action;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnemyWave {
    public EnemyWave(float ds, float de, Action s){
        spawn = s;
        delayStart = ds;
        delayEnd = de;
    }
    public Action spawn;
    public float delayStart;
    public float delayEnd;

}

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject beeEnemy;
    public GameObject bearEnemy;
    public GameObject monkeyEnemy;
    public GameObject crowEnemy;
    public GameObject guardEnemy;
    private EnemyWave[][] waves;

    public int level = 0;
    public float levelIncreasePeriod;
    private float timeSinceSpawn = 0;
    private float spawnerLifetime = 0;
    private EnemyWave currentWave;
    private EnemyWave nextWave;

    public bool infinite;

    void Start()
    {
        //set infinite = playerpref here

        EnemyWave[] level0 = new EnemyWave[]{
            new EnemyWave(3, 1, () => spawnGuardSwarm(1)),
        };

        EnemyWave[] level1 = new EnemyWave[]{
            new EnemyWave(3, 1, () => spawnHoneyBear(1)),
        };

        EnemyWave[] level2 = new EnemyWave[]{
            new EnemyWave(1, 6, () => spawnGuardSwarm(2)),
            new EnemyWave(1, 5, () => spawnMonkeySwarm(1)),
            new EnemyWave(1, 10, () => spawnBearSwarm(1)),
        };

        EnemyWave[] level3 = new EnemyWave[]{
            new EnemyWave(1, 5, () => spawnCrowSwarm(1)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(2)),
            new EnemyWave(1,6, () => spawnGuardSwarm(2)),
            new EnemyWave(1, 10, () => spawnBearSwarm(1)),
        };

        EnemyWave[] level4 = new EnemyWave[]{
            new EnemyWave(1, 5, () => spawnCrowSwarm(1)),
            new EnemyWave(1, 10, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 7, () => spawnBearSwarm(1)),
            new EnemyWave(1, 20, () => spawnMonkeyChasing(5)),
        };

        EnemyWave[] level5 = new EnemyWave[]{
            new EnemyWave(1, 10, () => spawnHoneyBear(1)),
            new EnemyWave(1, 7, () => spawnMonkeyChasing(2)),
            new EnemyWave(1, 7, () => spawnGuardChasing(2)),
        };

        EnemyWave[] level6 = new EnemyWave[]{

        };

        EnemyWave[] level7 = new EnemyWave[]{

        };

        EnemyWave[] level8 = new EnemyWave[]{

        };

        EnemyWave[] level9 = new EnemyWave[]{

        };

        EnemyWave[] level10 = new EnemyWave[]{

        };

        EnemyWave[] level11 = new EnemyWave[]{

        };

        EnemyWave[] level12 = new EnemyWave[]{

        };

        EnemyWave[] level13 = new EnemyWave[]{

        };

        EnemyWave[] level14 = new EnemyWave[]{

        };

        EnemyWave[] level15 = new EnemyWave[]{

        };

        EnemyWave[] level16 = new EnemyWave[]{

        };

        EnemyWave[] level17 = new EnemyWave[]{

        };

        EnemyWave[] level18 = new EnemyWave[]{

        };

        EnemyWave[] level19 = new EnemyWave[]{

        };

        EnemyWave[] level20 = new EnemyWave[]{

        };

        EnemyWave[] levelInfinite = new EnemyWave[]{

        };

        waves = new EnemyWave[][]{level0, level1, level2, level3, level4, level5, level6,
            level7, level8, level9, level10, level11, level12, level13, level14, level15,
            level16, level17, level18, level19, level20, levelInfinite};

        currentWave = level0[Random.Range(0, level0.Length)];
        nextWave = level0[Random.Range(0, level0.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        spawnerLifetime += Time.deltaTime;

        
        level = Mathf.Min( Mathf.FloorToInt(spawnerLifetime / levelIncreasePeriod), waves.Length - 1);
        if (level >= waves.Length - 1 && !infinite) { } //set up end screen here


        if(timeSinceSpawn >= currentWave.delayEnd + nextWave.delayStart){
            EnemyWave[] currentLevelWaves = waves[level];

            currentWave = nextWave;
            nextWave = currentLevelWaves[Random.Range(0, currentLevelWaves.Length)];

            timeSinceSpawn = 0;
            currentWave.spawn();
        }
    }

    
    private GameObject spawnEnemy(GameObject e, Vector2 offset){

        Vector3 spawnPos = transform.position + new Vector3(offset.x, offset.y, 0);
        spawnPos.z = -5;

        return Instantiate(e, spawnPos, Quaternion.identity);

    }

    private GameObject[] spawnGroupEnemy(GameObject e, int count, float minY, float maxY, float offsetX, float rangeX){
        
        GameObject[] newEnemies = new GameObject[count];
        for (int i = 0; i < count; i++){
            float yOffset = Random.Range(minY, maxY);
            float xPos = (float)i * offsetX;
            float xOffset = Random.Range(xPos - rangeX/2, xPos + rangeX/2);
            
            newEnemies[i] = spawnEnemy(e, new Vector2(xOffset, yOffset));
        }
        return newEnemies;
    }
    private IEnumerator spawnEnemyWithDelay(GameObject e, Vector2 offset, float delayTime){
        yield return new WaitForSeconds(delayTime);
        spawnEnemy(e, offset);
    }
    private void spawnTimedGroupEnemy(GameObject e, Vector2 offset, int count, float interval){
        for(int i = 0; i < count; i++){
            StartCoroutine(spawnEnemyWithDelay(e, offset, i*interval));
        }
    }

    //single enemy swarms
    public void spawnBeeSwarm(int count){
        float initialYOffset = Random.Range(2f, 4f);
        spawnGroupEnemy(beeEnemy, count, initialYOffset + 1f, initialYOffset - 1f, 1f, 1f);
    }

    public void spawnGuardSwarm(int count){
        spawnGroupEnemy(guardEnemy, count, 0, 0, 2, 0);
    }

    public void spawnMonkeySwarm(int count){
        spawnTimedGroupEnemy(monkeyEnemy, Vector2.zero, count, 2f);
    }

    public void spawnCrowSwarm(int count){
        Vector2 offset = new Vector2(0f, 4f);
        spawnTimedGroupEnemy(crowEnemy, offset, count, 1f);
    }

    public void spawnBearSwarm(int count){
        spawnGroupEnemy(bearEnemy, count, 0,0, 3f, 2f);
    }


    //group enemy swarms

    public void spawnHoneyBear(int bearCount){
        spawnBeeSwarm(bearCount * 2);
        spawnTimedGroupEnemy(bearEnemy, new Vector2(10, 0), bearCount, 2f);
    }

    public void spawnGuardChasing(int guardCount){
        spawnBearSwarm(1);
        spawnTimedGroupEnemy(guardEnemy, new Vector2(2, 0), guardCount, 0.5f);
    }

    public void spawnMonkeyChasing(int monkeyCount){
        GameObject guard = spawnEnemy(guardEnemy, new Vector2(-2, 0));
        guard.GetComponent<EnemyManager>().baseSpeed = 0.6f;

        spawnMonkeySwarm(monkeyCount);
    }
}
