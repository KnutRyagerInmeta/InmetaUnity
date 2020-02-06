using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody body;
    Health health;
    public float acceleration = 1;
    public float angularAcceleration = 100;
    public float jumpAcceleration = 10;
    public float bouncity = 0.5f;

    private int currentFrame = 0;
    private int lastFrameCollided = -999;
    private Vector3 previousVelocity = default;
    private Vector3 startPosition = default;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
        startPosition = transform.position;
        health.OnDeath += Reset;

    }

    // Update is called once per frame
    void Update()
    {
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            if (TouchesGround()) body.AddTorque(new Vector3(0, 0, angularAcceleration));
            else body.AddForce(new Vector3(-acceleration, 0, 0));
        }
        // Move right
        if (Input.GetKey(KeyCode.D))
        {
            if (TouchesGround()) body.AddTorque(new Vector3(0, 0, -angularAcceleration));
            else body.AddForce(new Vector3(acceleration, 0, 0));
        }
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && TouchesGround())
        {
            body.AddForce(new Vector3(0, jumpAcceleration, 0) / Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.R)) Reset();
    }

    private void FixedUpdate()
    {
        currentFrame++;
        previousVelocity = body.velocity;
    }

    private bool TouchesGround() => lastFrameCollided > currentFrame - 3;

    void OnCollisionStay(Collision collision)
    {
        lastFrameCollided = currentFrame;
    }

    void OnCollisionEnter(Collision collision)
    {
        var collisionNormal = collision.contacts[0].normal;
        var bounceForce = bouncity * collisionNormal * Vector3.Project(previousVelocity, collisionNormal).magnitude / Time.deltaTime;
        body.AddForce(bounceForce);

        //lastFrameCollided = currentFrame;
        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        //if (collision.relativeVelocity.magnitude > 2)
        //    audioSource.Play();
    }

    private void Reset()
    {
        transform.position = startPosition;
        body.velocity = Vector3.zero;
    }
}
