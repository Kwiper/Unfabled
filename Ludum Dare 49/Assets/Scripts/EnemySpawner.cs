using Action = System.Action;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct EnemyWave {
    public EnemyWave(Action s, float sd){
        spawn = s;
        spawnDuration = sd;
    }
    public Action spawn;
    public float spawnDuration;

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
    private float timeSinceSpawn = float.PositiveInfinity;
    private float spawnerLifetime = 0;
    private EnemyWave currentWave;

    void Start()
    {
        EnemyWave[] level0 = new EnemyWave[]{
            new EnemyWave(() => spawnGuardSwarm(1), 7),
        };

        EnemyWave[] level1 = new EnemyWave[]{
            new EnemyWave(() => spawnGuardSwarm(2), 7),
            new EnemyWave(() => spawnMonkeySwarm(1), 7),
        };

        EnemyWave[] level2 = new EnemyWave[]{
            new EnemyWave(() => spawnGuardSwarm(2),6),
            new EnemyWave(() => spawnMonkeySwarm(1), 5),
            new EnemyWave(() => spawnBearSwarm(1), 10),
        };

        EnemyWave[] level3 = new EnemyWave[]{
            new EnemyWave(() => spawnCrowSwarm(1), 5),
            new EnemyWave(() => spawnMonkeySwarm(2), 2),
            new EnemyWave(() => spawnGuardSwarm(2),6),
            new EnemyWave(() => spawnBearSwarm(1), 10),
        };

        EnemyWave[] level4 = new EnemyWave[]{
            new EnemyWave(() => spawnCrowSwarm(1), 5),
            new EnemyWave(() => spawnBeeSwarm(2), 10),
            new EnemyWave(() => spawnBearSwarm(1), 7),
            new EnemyWave(() => spawnMonkeyChasing(5), 20),
        };

        EnemyWave[] level5 = new EnemyWave[]{
            new EnemyWave(() => spawnHoneyBear(1), 10),
            new EnemyWave(() => spawnMonkeyChasing(2), 7),
            new EnemyWave(() => spawnGuardChasing(2), 7),
        };

        waves = new EnemyWave[][]{level0, level1, level2, level3, level4, level5};

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        spawnerLifetime += Time.deltaTime;

        
        level = Mathf.Min( Mathf.FloorToInt(spawnerLifetime / levelIncreasePeriod), waves.Length - 1);


        if(timeSinceSpawn >= currentWave.spawnDuration){
            EnemyWave[] currentLevelWaves = waves[level];

            EnemyWave newWave = currentLevelWaves[Random.Range(0, currentLevelWaves.Length)];

            executeEnemyWave(newWave);
        }
    }

    private void executeEnemyWave(EnemyWave wave){
        timeSinceSpawn = 0;
        currentWave = wave;

        wave.spawn();
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
