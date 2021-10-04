using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Text start;
    [SerializeField] Text quit;
    [SerializeField] Text ld;

    [SerializeField] OnHover startHover;
    [SerializeField] OnHover quitHover;


    float fadeTimer = 1f;

    float alpha = 0f;

    // Start is called before the first frame update
    void Start()
    {

        start.color = new Color(1, 1, 1, alpha);
        quit.color = new Color(1, 1, 1, alpha);
        ld.color = new Color(1, 1, 1, alpha);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer > 0) {
            fadeTimer -= Time.deltaTime;
        }

        if (fadeTimer <= 0)
        {
            start.color = new Color(startHover.RGB, startHover.RGB, startHover.RGB, alpha);
            quit.color = new Color(quitHover.RGB, quitHover.RGB, quitHover.RGB, alpha);
            ld.color = new Color(1, 1, 1, alpha);

            alpha += Time.deltaTime * 0.75f;

            Mathf.Clamp(alpha, 0, 1);
        }

    }
}
