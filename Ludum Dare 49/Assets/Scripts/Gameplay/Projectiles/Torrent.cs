using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torrent : MonoBehaviour
{
    private int count = 0;
    [SerializeField] GameObject toFire;
    [SerializeField] float delay;
    // Start is called before the first frame update
    void Start()
    {
        fireProjectile();
    }

    // Update is called once per frame
    void fireProjectile()
    {
        Instantiate(toFire, transform.position, transform.rotation);
        if (count < 7)
        {
            Invoke("fireProjectile", delay);
            count++;
        }
    }
}
