using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float knockback;
    [SerializeField] bool lingering;
    public bool toDestroy = false;

    //particles
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] GameObject castEffect;

    private GameObject cast;

    void Start()
    {
        //if (bulletTrail != null) bulletTrail.Play();
        if (castEffect != null) cast = Instantiate(castEffect, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().getFirePoint());
        cast.SetActive(true);
    }

    void FixedUpdate()
    {
        if (toDestroy) Destroy(gameObject);
    }

    public float getKnockback()
    {
        return knockback;
    }

    public float getDamage()
    {
        return damage;
    }

    public bool isLingering()
    {
        return lingering;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage); //enemy takes damage
        }
    }
}

