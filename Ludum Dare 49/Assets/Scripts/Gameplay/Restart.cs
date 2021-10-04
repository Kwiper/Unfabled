using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    OnHover hover;

    [SerializeField] bool setInfinite;
    public bool infinite;

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
                infinite = PlayerPrefs.GetInt("Infinite", 0) == 1;

                if (infinite)
                {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetInt("Infinite", 1);
                }
                else {
                    PlayerPrefs.DeleteAll();
                    PlayerPrefs.SetInt("Infinite", 0);
                }

                SceneManager.LoadScene(1);
            }
        }
    }
}
