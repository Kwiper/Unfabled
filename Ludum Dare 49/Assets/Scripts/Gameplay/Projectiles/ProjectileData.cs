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
    [SerializeField] ParticleSystem bulletTrail;
    [SerializeField] ParticleSystem hitEffect;

    void Start()
    {
        bulletTrail.Play();
    }

    void FixedUpdate()
    {
        if (toDestroy) DestroyBullet(0f);
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

    public void DestroyBullet(float delay)
    {
        Destroy(bulletTrail);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(damage); //enemy takes damage
        }
    }
}

