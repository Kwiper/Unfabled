using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] GameObject toFire;
    float startHeight;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        startHeight = GameObject.FindGameObjectWithTag("Ground").GetComponent<CompositeCollider2D>().bounds.max.y - 0.5f;

        Instantiate(toFire, new Vector3(worldPosition.x, startHeight, 0), Quaternion.identity);
        Destroy(gameObject);
        //Transform cursorPos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().cursor;
    }
}
