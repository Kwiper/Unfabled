using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    [SerializeField] GameObject logo;
    [SerializeField] Canvas mainMenu;
    [SerializeField] Canvas canvasToOpen;

    OnHover hover;

    // Start is called before the first frame update
    void Start()
    {
        hover = GetComponent<OnHover>();

    }

    // Update is called once per frame
    void Update()
    {

        if (hover.GetMouseHover())
        {
            if (Input.GetMouseButtonUp(0))
            {
                logo.SetActive(false);
                mainMenu.gameObject.SetActive(false);
                canvasToOpen.gameObject.SetActive(true);
            }
        }
    }
}
