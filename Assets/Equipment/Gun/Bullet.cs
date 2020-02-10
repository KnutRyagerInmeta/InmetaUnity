using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] protected float expirationTime = 10;   // Seconds
    [SerializeField] protected float damage = 10;
    [SerializeField] protected Explosive explosive;
    [SerializeField] protected bool disappearOnImpact;
    [SerializeField] protected bool isSticky;
    private Rigidbody body;

    // Use this for initialization
    void Start()
    {
        body = GetComponentInChildren<Rigidbody>();
        explosive = GetComponentInChildren<Explosive>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject;
        var hp = target.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
        }
        if (disappearOnImpact)
        {
            Die();
        }
    }

    public void Init(Transform transform)
    {
        body = GetComponentInChildren<Rigidbody>();
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }

    public void Die()
    {
        if (explosive != null)
        {
            explosive.Explode();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void Shoot(float force)
    {
        var f = transform.forward.normalized * force;
        body.AddForce(f, ForceMode.Impulse);
    }
}
