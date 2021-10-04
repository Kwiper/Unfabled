using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHover : MonoBehaviour
{

    Text text;
    Collider2D collider2D;

    float rgb = 1;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetMouseHover())
        {
            if (!Input.GetMouseButton(0))
            {
                if (rgb > 0.5f)
                {
                    rgb -= Time.deltaTime * 2f;
                }
                else if (rgb < 0.5f) {
                    rgb += Time.deltaTime * 2f;
                }

            }
            else{
                if (rgb > 0.33f) {
                    rgb -= Time.deltaTime * 2f;
                }
            }
        }
        else {
            if (rgb < 1)
            {
                rgb += Time.deltaTime * 2f;
            }
        }

    }

    public bool GetMouseHover() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (collider2D.OverlapPoint(mousePosition)){
            return true;
        }

        return false;
    }

    public float RGB => rgb;

}
