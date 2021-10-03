using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireOnDelay : MonoBehaviour
{
    [SerializeField] GameObject toFire;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("fireProjectile", delay);
    }

    void fireProjectile()
    {
        Instantiate(toFire, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
