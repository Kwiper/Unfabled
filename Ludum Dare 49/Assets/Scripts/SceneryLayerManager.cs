using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SceneryLayerManager : MonoBehaviour
{
    public int layer;  


    private SpriteRenderer spriteRd;
    private Vector3 tileSize;
    private Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {   

        originPos = transform.localPosition;
        originPos.z = - layer;
        transform.localPosition = originPos;

        spriteRd = GetComponent<SpriteRenderer>();
        
        tileSize = spriteRd.sprite.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        //scroll background
        float speed = GetComponentInParent<BackgroundManager>().speed;
        float dx = speed * layer * Time.deltaTime; 
        transform.Translate(dx, 0f, 0f);
        

        //reset position if moved outside of bounds
        float pos = transform.localPosition.x - originPos.x;
        float localSize = tileSize.x * transform.localScale.x;
        if( Mathf.Abs(pos) >= Mathf.Abs(localSize) ){
            transform.localPosition = originPos;
        }
    }
}
