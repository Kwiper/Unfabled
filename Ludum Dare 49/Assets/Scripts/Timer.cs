using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    PlayerController player;
    private float startTime;
    private float timeElapsed;
    public string minutes;
    public string seconds;


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        float resetTime = PlayerPrefs.GetFloat("Time", 0f); 
        startTime = Time.time - resetTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Health > 0) {
            timeElapsed = Time.time - startTime;

            minutes = ((int)timeElapsed / 60).ToString();
            seconds = (timeElapsed % 60).ToString("f2");

            timerText.text = minutes + ":" + seconds;
        }
    }

    public float TimeElapsed => timeElapsed;
}
