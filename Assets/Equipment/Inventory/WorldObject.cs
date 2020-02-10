using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour, Initiable
{

    protected Rigidbody rigidBody;

    public virtual void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody>();
    }

    public void Update()
    {
    }


    public void SetPosition(Vector3 position)
    {
        this.transform.position = position;
    }

    public void SetTransform(Transform transform)
    {
        this.transform.position = transform.position;
        this.transform.rotation = transform.rotation;
    }

    public void RotateY(float speed)
    {
        //transform.rotate
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
    }

    public void SetScale(float scale)
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetScaleX(float scale)
    {
        this.transform.localScale = new Vector3(scale, transform.localScale.x, transform.localScale.z);
    }

    public void SetScaleY(float scale)
    {
        this.transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);
    }

    public void SetScaleZ(float scale)
    {
        this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, scale);
    }

    public void Init()
    {
        if (rigidBody != null)
        {
            Stop();
        }
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
}
