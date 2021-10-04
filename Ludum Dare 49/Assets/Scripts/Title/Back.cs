using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back : MonoBehaviour
{

    [SerializeField] GameObject logo;
    [SerializeField] Canvas mainMenu;
    [SerializeField] Canvas canvasToClose;
    [SerializeField] Text text;

    OnHover hover;

    // Start is called before the first frame update
    void Start()
    {
        hover = GetComponent<OnHover>();
        text.color = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        text.color = new Color(hover.RGB, hover.RGB, hover.RGB);

        if (hover.GetMouseHover())
        {
            if (Input.GetMouseButtonUp(0))
            {
                logo.SetActive(true);
                mainMenu.gameObject.SetActive(true);
                canvasToClose.gameObject.SetActive(false);
            }
        }
    }
}
