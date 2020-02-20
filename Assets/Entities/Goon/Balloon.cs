using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Balloon : MonoBehaviour
{
    public float upwardsForce;
    public Rigidbody body;
    public Explosive explosive;
    public float explodeThreshold;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        body.AddForce(Vector3.up * upwardsForce);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.impulse.magnitude > explodeThreshold)
        {
            explosive.Explode();
        }
    }
}
