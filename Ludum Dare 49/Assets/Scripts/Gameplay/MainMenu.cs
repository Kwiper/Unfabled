using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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
                SceneManager.LoadScene(0);
            }
        }
    }
}
