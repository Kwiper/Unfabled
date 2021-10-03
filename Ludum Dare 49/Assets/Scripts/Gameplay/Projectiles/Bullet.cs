using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float verticalNudge;
    [SerializeField] bool randomizeNudge;

    //parameters
    [SerializeField] bool applyPhysics;
    [SerializeField] bool bounce;
    [SerializeField] bool goThroughGround;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (randomizeNudge) verticalNudge = Random.Range(-verticalNudge, verticalNudge);
        rb.velocity = transform.up * bulletSpeed + transform.right * verticalNudge;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage); //enemy takes damage
        }
        if (col.gameObject.tag == "Ground")
        {
            if(bounce && rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * -1);
            else if (!goThroughGround) Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
