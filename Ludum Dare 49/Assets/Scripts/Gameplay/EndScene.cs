using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{

    [SerializeField] OnHover menuHover;
    [SerializeField] Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Color(menuHover.RGB, menuHover.RGB, menuHover.RGB);
    }
}
