using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalHover : MonoBehaviour
{
    [SerializeField] Text survival;
    [SerializeField] OnHover survivalHover;

    // Start is called before the first frame update
    void Start()
    {
        survival.color = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        survival.color = new Color(survivalHover.RGB, survivalHover.RGB, survivalHover.RGB);
    }
}
