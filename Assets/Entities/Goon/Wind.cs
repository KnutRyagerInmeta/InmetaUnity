using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] ParticleSystem particleSystem;
    private float x, y, z;
    private Vector3 velocity;
    private float power = 0.01f;
    private Vector3 velocityTimesPower;
    private Vector3 hitbox;
    private Vector3 middlePosition;

    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        x = particleSystem.shape.scale.x;
        y = particleSystem.shape.scale.y;
        z = particleSystem.main.startLifetime.constant * particleSystem.main.startSpeed.constant;
        hitbox = transform.rotation * new Vector3(x, y, z);
        velocity = transform.forward.normalized * z;
        velocityTimesPower = velocity * power;
        hitbox.x = Mathf.Abs(hitbox.x);
        hitbox.y = Mathf.Abs(hitbox.y);
        hitbox.z = Mathf.Abs(hitbox.z);
        middlePosition = transform.position + new Vector3(0, 0, hitbox.z * 0.5f);
        //middlePosition = transform.localScale / 2;
        Debug.Log("wind: " + x + "," + particleSystem.main.startLifetime.constant + " :" + particleSystem.main.startSpeed.constant + ", hitbox: " + hitbox + "," + transform.position + ";" + middlePosition);
    }

    bool wasCol;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (wasCol)
            Gizmos.DrawCube(middlePosition, hitbox);
    }

    void FixedUpdate()
    {
        var colliders = Physics.OverlapBox(middlePosition, hitbox, Quaternion.identity);
        //Debug.Log("colliders: " + colliders.Length + ",");
        wasCol = colliders.Length > 1;
        for (var i = 0; i < colliders.Length; i++)
        {
            var collider = colliders[i];
            var target = collider.gameObject;

            var body = target.GetComponentInChildren<Rigidbody>() ?? target.GetComponentInParent<Rigidbody>();
            if (body != null)
            {
                var velocityDifference = velocity - Vector3.Project(body.velocity, velocity);
                var force = velocityDifference * power;
                body.AddForce(force, ForceMode.Force);
            }

            var parachute = target.GetComponentInChildren<Parachute>() ?? target.GetComponentInParent<Parachute>();
            if (parachute != null)
            {
                parachute.ApplyForce(velocityTimesPower);
            }
        }
    }
}