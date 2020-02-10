using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{

    [SerializeField] Collider deathCollider;

    void Start()
    {
        deathCollider = GetComponentInChildren<Collider>();
        var mesh = GetComponentInChildren<MeshRenderer>();
        if (mesh != null)
        {
            mesh.enabled = false;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.gameObject;
        Health health = gameObject.GetComponentInChildren<Health>();
        if(health != null)
        {
            health.Die();
        }

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }
}
