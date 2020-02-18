using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody body;
    Health health;
    public float airAcceleration = 1;
    public float brakeStrength = 2;
    public float angularAcceleration = 100;
    public float jumpSpeed = 10;
    public float bouncity = 0.5f;

    private int currentFrame = 0;
    private int lastFrameCollided = -999;
    private Vector3 previousVelocity = default;
    private Vector3 startPosition = default;
    private float jumpTimer = 0;
    private float jumpTime = 0.3f;

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
        var moveDirection = 0f;

        if (Input.GetKey(KeyCode.A)) moveDirection -= 1; // Move left
        if (Input.GetKey(KeyCode.D)) moveDirection += 1; // Move right
        if (Input.GetKey(KeyCode.S) && moveDirection == 0) moveDirection = -Mathf.Sign(body.velocity.x);    // brake

        if (moveDirection != 0)
        {
            var useGroundAcceleration = TouchesGround() && !Input.GetKey(KeyCode.S);
            var horizontalDirection = Mathf.Sign(body.velocity.x);
            var accelerationGroundOrAir = useGroundAcceleration ? -angularAcceleration : airAcceleration;
            var accelerationWithDirection = moveDirection * accelerationGroundOrAir;
            var accelerationWithBraking = horizontalDirection == moveDirection ? accelerationWithDirection : accelerationWithDirection * brakeStrength;
            var brakeLimit = Mathf.Abs(accelerationWithBraking) < Mathf.Abs(body.velocity.x * Time.deltaTime) ? accelerationWithBraking : -body.velocity.x;
            var accelerationWithBrakeLimit = Input.GetKey(KeyCode.S) ? brakeLimit : accelerationWithBraking;
            if (useGroundAcceleration) body.AddTorque(new Vector3(0, 0, accelerationWithBrakeLimit), ForceMode.Acceleration);
            else body.AddForce(Vector3.right * accelerationWithBrakeLimit, ForceMode.Acceleration);
        }
        // Jump
        jumpTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && TouchesGround() && jumpTimer >= jumpTime)
        {
            var verticalDirection = Mathf.Sign(body.velocity.y);
            var currentUpSpeed = verticalDirection == 1 ? body.velocity.y : -body.velocity.y * bouncity;
            if (currentUpSpeed < jumpSpeed) body.AddForce(Vector3.up * ((jumpSpeed - currentUpSpeed)), ForceMode.Impulse);
            jumpTimer = 0;
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
