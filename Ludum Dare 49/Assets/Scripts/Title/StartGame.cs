using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    OnHover hover;
    [SerializeField] bool setInfinite;
    [SerializeField] bool retainState;
    int setInfInt;

    // Start is called before the first frame update
    void Start()
    {
        hover = GetComponent<OnHover>();
        setInfInt = setInfinite ? 1 : 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hover.GetMouseHover()) {
            if (Input.GetMouseButtonUp(0)) {
                if(!retainState) PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("Infinite", setInfInt);
                SceneManager.LoadScene(1);
            }
        }
    }
}
