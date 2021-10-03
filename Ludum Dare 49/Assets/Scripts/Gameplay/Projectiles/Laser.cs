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

    [SerializeField] bool stayProjectile;
    [SerializeField] float stayTimer;
    Vector2 growRate;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Tiled;
        //sr.size = new Vector2(0.32f, 1.08f);
        growRate = new Vector2(growRateX, growRateY);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        duration -= Time.deltaTime;
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (!stayProjectile)
        {
            float bgSpeed = GameObject.FindGameObjectWithTag("Background").GetComponent<BackgroundManager>().speed;
            float layerSpeed = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundLayerManager>().layer;
            float dx = bgSpeed * layerSpeed * Time.deltaTime;
            transform.Translate(dx, 0f, 0f);
        }
        if (duration > 0 && sr.bounds.max.x < stageDimensions.x * 1.25 && sr.bounds.max.y < stageDimensions.y * 1.25)
        {
            sr.size += growRate; //grow rate
            growRate = new Vector2(growRate.x * growAccel, growRate.y * growAccel); //grow acceleration
        }
        else if(duration < 0)
        {
            if (stayProjectile)
            {
                transform.localScale -= transform.up * 0.5f;
                if(transform.localScale.y <= 0 || transform.localScale.x <= 0) Destroy(gameObject);
            }
            else
            {
                if (stayTimer > 0) stayTimer -= Time.deltaTime;
                else Destroy(gameObject);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
