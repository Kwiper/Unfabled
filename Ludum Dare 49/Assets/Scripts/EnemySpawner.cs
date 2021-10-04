using Action = System.Action;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<float> levelIncreasePeriods;
    private List<float> levelIncreaseTimes = new List<float>();
    private float timeSinceSpawn = 0;
    private float spawnerLifetime = 0;
    private EnemyWave currentWave;
    private EnemyWave nextWave;

    public bool infinite;

    void Start()
    {
        //set infinite = playerpref here
        infinite = PlayerPrefs.GetInt("Infinite", 0) == 1;

        EnemyWave[] level0 = new EnemyWave[]{
            new EnemyWave(0, 5, () => spawnGuardSwarm(1)),
        };

        EnemyWave[] level1 = new EnemyWave[]{
            new EnemyWave(2, 12, () => spawnMonkeyBear(2)),
        };

        EnemyWave[] level2 = new EnemyWave[]{
            new EnemyWave(1, 4, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 4, () => spawnCrowSwarm(2)),
        };

        EnemyWave[] level3 = new EnemyWave[]{
            new EnemyWave(2, 2, () => spawnBearSwarm(2)),
            new EnemyWave(2, 5, () => spawnGuardSwarm(1)),
            new EnemyWave(2, 5, () => spawnCrowSwarm(2)),
        };

        EnemyWave[] level4 = new EnemyWave[]{
            new EnemyWave(2, 4, () => spawnBeeSwarm(1)),
            new EnemyWave(3, 4, () => spawnMonkeyChasing(2)),
            new EnemyWave(2, 4, () => spawnBeeSwarm(1)),
        };

        EnemyWave[] level5 = new EnemyWave[]{
            new EnemyWave(1, 4, () => spawnHoneyBear(2)),
            new EnemyWave(2, 4, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 4, () => spawnHoneyBear(1)),
        };

        EnemyWave[] level6 = new EnemyWave[]{
            new EnemyWave(1, 4, () => spawnBeeSwarm(1)),
            new EnemyWave(3, 4, () => spawnMonkeyChasing(3)),
            new EnemyWave(1, 4, () => spawnBeeSwarm(1)),
        };

        EnemyWave[] level7 = new EnemyWave[]{
            new EnemyWave(1, 4, () => spawnBearSwarm(2)),
            new EnemyWave(3, 4, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 4, () => spawnBearSwarm(2)),
        };

        EnemyWave[] level8 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
            new EnemyWave(1, 3, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
            new EnemyWave(1, 3, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
        };

        EnemyWave[] level9 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnHoneyBear(1)),
            new EnemyWave(1, 3, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 2, () => spawnHoneyBear(1)),
            new EnemyWave(1, 3, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 2, () => spawnHoneyBear(1)),
        };

        EnemyWave[] level10 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(1)),
            new EnemyWave(1, 2, () => spawnGuardSwarm(1)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(1)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
        };

        EnemyWave[] level11 = new EnemyWave[]{
            new EnemyWave(4, 4, () => spawnBeeSwarm(3)),
            new EnemyWave(1, 6, () => spawnBearSwarm(3)),
            new EnemyWave(2, 3, () => spawnBearSwarm(5)),
        };

        EnemyWave[] level12 = new EnemyWave[]{
            new EnemyWave(4, 2, () => spawnGuardSwarm(2)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 2, () => spawnGuardSwarm(2)),
        };

        EnemyWave[] level13 = new EnemyWave[]{
            new EnemyWave(4, 2, () => spawnMonkeySwarm(2)),
            new EnemyWave(1, 2, () => spawnHoneyBear(2)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(1)),
            new EnemyWave(1, 2, () => spawnHoneyBear(2)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(1)),
        };

        EnemyWave[] level14 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnCrowSwarm(1)),
            new EnemyWave(1, 3, () => spawnMonkeyChasing(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(1)),
            new EnemyWave(1, 3, () => spawnMonkeyChasing(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(1)),
        };

        EnemyWave[] level15 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnHoneyBear(1)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(2)),
            new EnemyWave(1, 2, () => spawnHoneyBear(2)),
            new EnemyWave(1, 2, () => spawnGuardChasing(1)),
            new EnemyWave(1, 2, () => spawnHoneyBear(1)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(3)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(2)),
        };

        EnemyWave[] level16 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(1)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(1)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 3, () => spawnGuardSwarm(1)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(3)),
        };

        EnemyWave[] level17 = new EnemyWave[]{
            new EnemyWave(1, 3, () => spawnMonkeyChasing(2)),
            new EnemyWave(1, 3, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 3, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 3, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 3, () => spawnBeeSwarm(2)),
            new EnemyWave(1, 3, () => spawnMonkeyChasing(2)),
        };

        EnemyWave[] level18 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnGuardChasing(2)),
            new EnemyWave(1, 3, () => spawnHoneyBear(2)),
            new EnemyWave(1, 3, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 3, () => spawnMonkeySwarm(2)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(1)),
            new EnemyWave(1, 2, () => spawnMonkeyChasing(1)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(2)),
        };

        EnemyWave[] level19 = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnHoneyBear(2)),
            new EnemyWave(1, 2, () => spawnMonkeyChasing(2)),
            new EnemyWave(1, 2, () => spawnCrowSwarm(3)),
            new EnemyWave(1, 1, () => spawnGuardChasing(2)),
            new EnemyWave(1, 2, () => spawnBeeSwarm(3)),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(2)),
            new EnemyWave(1, 1, () => spawnCrowSwarm(2)),
            new EnemyWave(1, 1, () => spawnBearSwarm(1)),
            new EnemyWave(1, 1, () => spawnGuardSwarm(1)),
            new EnemyWave(1, 1, () => spawnMonkeyChasing(1)),
        };

        EnemyWave[] levelInfinite = new EnemyWave[]{
            new EnemyWave(1, 2, () => spawnHoneyBear(Random.Range(0, 4))), 
            new EnemyWave(1, 2, () => spawnMonkeyChasing(Random.Range(0, 4))),
            new EnemyWave(1, 2, () => spawnCrowSwarm(Random.Range(0, 5))),
            new EnemyWave(1, 1, () => spawnGuardChasing(Random.Range(0, 4))),
            new EnemyWave(1, 2, () => spawnBeeSwarm(Random.Range(0, 4))),
            new EnemyWave(1, 2, () => spawnMonkeySwarm(Random.Range(0, 3))),
            new EnemyWave(1, 1, () => spawnCrowSwarm(Random.Range(0, 4))),
            new EnemyWave(1, 1, () => spawnBearSwarm(Random.Range(0, 2))),
            new EnemyWave(1, 1, () => spawnGuardSwarm(Random.Range(0, 2))),
            new EnemyWave(1, 1, () => spawnMonkeyChasing(Random.Range(0, 3))),
        };

        waves = new EnemyWave[][]{level0, level1, level2, level3, level4, level5, level6,
            level7, level8, level9, level10, level11, level12, level13, level14, level15,
            level16, level17, level18, level19, levelInfinite};

        currentWave = level0[Random.Range(0, level0.Length)];
        nextWave = level0[Random.Range(0, level0.Length)];

        float time = 0;
        for(int i = 0; i < waves.Length; i++){
            if(i < levelIncreasePeriods.Count){
                time += levelIncreasePeriods[i];
            }else{
                time += levelIncreasePeriods.Last();
            }
            levelIncreaseTimes.Add(time);
        }

    }

    // Update is called once per frame
    private int getLevel(){
        for(int i = waves.Length - 1; i > 0; i--){
            if(spawnerLifetime > levelIncreaseTimes[i - 1]){
                return i;
            }
        }
        return 0;

    }
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        spawnerLifetime += Time.deltaTime;

        
        level = getLevel();

        // level = Mathf.Min( Mathf.FloorToInt(spawnerLifetime / levelIncreasePeriod), waves.Length - 1);


        if (level >= waves.Length - 1 && !infinite) {

        } //set up end screen here


        if(timeSinceSpawn >= currentWave.delayEnd + nextWave.delayStart){
            EnemyWave[] currentLevelWaves = waves[level];

            currentWave = nextWave;
            nextWave = currentLevelWaves[Random.Range(0, currentLevelWaves.Length - 1)];

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

    public void spawnMonkeyBear(int count){
        spawnMonkeySwarm(count);
        spawnTimedGroupEnemy(bearEnemy, new Vector2(10, 0), count, 4f);
    }
}
