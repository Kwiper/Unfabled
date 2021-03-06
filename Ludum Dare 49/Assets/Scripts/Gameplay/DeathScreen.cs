using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToTurnOff;
    [SerializeField] List<GameObject> objectsToTurnOn;
    [SerializeField] Timer timer;
    [SerializeField] PlayerController player;

    [SerializeField] Text timerText;

    [SerializeField] Text restart;
    [SerializeField] Text menu;

    [SerializeField] OnHover restartHover;
    [SerializeField] OnHover menuHover;

    string minutes;
    string seconds;

    public bool infinite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        infinite = PlayerPrefs.GetInt("Infinite", 0) == 1;

        if (player.Health <= 0) {
            minutes = timer.minutes;
            seconds = timer.seconds;

            for (int i = 0; i < objectsToTurnOff.Count; i++) {
                objectsToTurnOff[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < objectsToTurnOn.Count; i++) {
                objectsToTurnOn[i].gameObject.SetActive(true);
            }

            if (infinite)
            {
                timerText.text = "You survived for " + minutes + ":" + seconds;
            }
            else {
                timerText.text = "The forest consumed you alive!";
            }

            restart.color = new Color(restartHover.RGB, restartHover.RGB, restartHover.RGB);
            menu.color = new Color(menuHover.RGB, menuHover.RGB, menuHover.RGB);

        }
    }
}
