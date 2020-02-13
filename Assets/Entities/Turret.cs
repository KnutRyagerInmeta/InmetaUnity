using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform target;
    public Gun gun;
    public float sightRange;

    void Start()
    {
        gun = GetComponent<Gun>();
    }

    void Update()
    {
        if (target == null) Scout();
        if (target != null)
        {
            Aim();
            gun.Fire();
        }
    }

    void Scout()
    {
        target = null;
        var player = FindObjectOfType<Ball>();
        if(player != null && Vector3.Distance(transform.position, player.transform.position) <= sightRange)
        {
            target = player.transform;
        }
    }

    void Aim()
    {

    }
}
