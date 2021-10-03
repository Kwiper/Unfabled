using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    PlayerController player;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Health > 0) {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
        }
    }
}
