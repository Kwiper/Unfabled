using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float knockback;
    [SerializeField] bool lingering;

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
}
