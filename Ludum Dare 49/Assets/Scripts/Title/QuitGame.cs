using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
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
                Application.Quit();
                Debug.Log("Quitting!");
            }
        }
    }
}
