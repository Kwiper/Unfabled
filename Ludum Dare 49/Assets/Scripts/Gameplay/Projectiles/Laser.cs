using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] float growRateX;
    [SerializeField] float growRateY;
    [SerializeField] float growAccel;
    [SerializeField] float duration;
    Vector2 growRate;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
        sr.size = new Vector2(0.32f, 1.08f);
        growRate = new Vector2(growRateX, growRateY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sr.size.y <= 5)
        {
            sr.size += growRate; //grow rate
            growRate = new Vector2(growRate.x * growAccel, growRate.y * growAccel); //grow acceleration
        }
    }
}
