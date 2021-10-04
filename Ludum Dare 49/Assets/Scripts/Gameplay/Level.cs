using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] Text text;

    public bool infinite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        infinite = PlayerPrefs.GetInt("Infinite", 0) == 1;

        if (infinite)
        {
            text.text = "Level: " + spawner.level;
        }
        else {
            text.text = "Level: " + spawner.level + "/20";
        }

    }
}
