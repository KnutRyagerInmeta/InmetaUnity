using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : FollowEquipment
{

    [SerializeField] Bullet bulletType;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float fireRate = 1;    // Shots per second
    [SerializeField] float force = 100;
    [SerializeField] float variance = 0;
    [SerializeField] int ammo;
    [SerializeField] int maxAmmo = 10;
    [SerializeField] int bulletCount = 1;
    [SerializeField] bool auto;
    [SerializeField] KeyCode fireKey;
    [SerializeField] AudioClip[] sounds;
    private float lastFireTime = -1;
    Audio source;



    // Use this for initialization
    protected void Start()
    {
        source = GetComponentInChildren<Audio>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        SetPositionToTarget();
        if (fireKey != default(KeyCode) && (Input.GetMouseButtonDown(0) || (auto && Input.GetMouseButton(0))))
        {
            Fire();
        }
    }

    public void Fire()
    {
        if (CanFire())
        {
            var shots = Math.Min(ammo, bulletCount);
            ammo -= shots;
            for (var i = 0; i < shots; i++)
            {
                var bullet = Instantiate<Bullet>(bulletType);
                bullet.Init(bulletSpawn);
                bullet.Shoot(force);
                OnFire(bullet);
            }
            lastFireTime = Time.realtimeSinceStartup;
            source.Play(sounds);
        }
    }

    protected virtual void OnFire(Bullet bullet)
    {

    }

    public void Reload()
    {
        ammo = maxAmmo;
    }

    private bool CanFire()
    {
        return ammo > 0 && (Time.realtimeSinceStartup - lastFireTime >= fireRate);
    }
}
