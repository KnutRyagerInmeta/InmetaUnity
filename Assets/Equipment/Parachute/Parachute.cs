using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : FollowEquipment
{

    bool active;
    [SerializeField] Rigidbody targetBody;
    [SerializeField] float strength = 1;
    [SerializeField] float bendSpeed = 0.5f;
    [SerializeField] float resetSpeed = 0.25f;
    [SerializeField] float minBend = -Mathf.PI / 4;
    [SerializeField] float maxBend = Mathf.PI / 4;
    float bendX;
    float bendZ;
    Vector3 angle;
    float lastActive;


    // Use this for initialization
    void Start()
    {
        angle = Vector3.up;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Damp();
        var bendXAbs = Mathf.Abs(bendX);
        var bendZAbs = Mathf.Abs(bendZ);
        var cosX = Mathf.Cos(bendXAbs);
        var downAngle = bendXAbs + bendZAbs * cosX;
        var cosDown = Mathf.Cos(downAngle);
        var y = cosDown;
        //angle = transform.rotation.eulerAngles;
        angle = new Vector3(bendX, y, bendZ);
        var force = (-Vector3.Project(targetBody.velocity, angle)) * strength * targetBody.mass;
        //Debug.Log("para force: " + force + ", angle: " + angle + ",vel: " + targetBody.velocity);
        targetBody.AddForce(force);
        transform.rotation = Quaternion.Euler(bendZ * 180 / Mathf.PI, 0, -bendX * 180 / Mathf.PI);
    }

    private void Damp()
    {
        if (bendX != 0 || bendZ != 0)
        {
            setBend(Time.deltaTime);
            //Debug.Log("RESET:" + resetX + ", " + resetZ + ", " + bendX + "," + bendZ + ", ");
        }

    }

    private void setBend(float deltaTime)
    {
        var resetSpeedNow = deltaTime * resetSpeed;
        var angleOf = Mathf.Atan2(angle.z, angle.x);
        var cos = Mathf.Cos(angleOf);
        var sin = Mathf.Sin(angleOf);
        var resetX = -resetSpeedNow * cos;
        var resetZ = -resetSpeedNow * sin;
        bendX += resetX;
        bendZ += resetZ;
        if (resetX > 0)
        {
            bendX = Mathf.Min(bendX, 0);
        }
        else if (resetX < 0)
        {
            bendX = Mathf.Max(bendX, 0);
        }
        if (resetZ > 0)
        {
            bendZ = Mathf.Min(bendZ, 0);
        }
        else if (resetZ < 0)
        {
            bendZ = Mathf.Max(bendZ, 0);
        }
    }

    internal void ApplyForce(Vector3 force)
    {
        force = (Vector3.Project(force, angle)) * strength * targetBody.mass;
        //Debug.Log("para force: " + force + ", angle: " + angle + ",vel: " + targetBody.velocity);
        targetBody.AddForce(force);
        transform.rotation = Quaternion.Euler(bendZ * 180 / Mathf.PI, 0, -bendX * 180 / Mathf.PI);
    }

    internal void Disable()
    {
        if (gameObject.activeInHierarchy)
        {
            Toggle();
        }
    }

    internal void Toggle()
    {
        SetPositionToTarget();
        gameObject.SetActive(!gameObject.activeInHierarchy);
        if (gameObject.activeInHierarchy)
        {
            float deltaTime = Time.realtimeSinceStartup - lastActive;
            setBend(deltaTime);
        }
        lastActive = Time.realtimeSinceStartup;
    }

    public void Bend(Vector3 move)
    {
        if (move == Vector3.zero)
        {
            return;
        }
        var bendSpeedNow = Time.deltaTime * bendSpeed;
        var angleOf = Mathf.Atan2(move.z, move.x);
        var cos = Mathf.Cos(angleOf);
        var sin = Mathf.Sin(angleOf);
        var xComponent = bendSpeedNow * cos;
        var zComponent = bendSpeedNow * sin;
        bendX += xComponent;
        bendZ += zComponent;
        var xComponentAbs = Mathf.Abs(cos);
        var zComponentAbs = Mathf.Abs(sin);
        var currentMinBendX = minBend * xComponentAbs;
        var currentMinBendZ = minBend * zComponentAbs;
        var currentMaxBendX = maxBend * xComponentAbs;
        var currentMaxBendZ = maxBend * zComponentAbs;
        bendX = Mathf.Clamp(bendX, currentMinBendX, currentMaxBendX);
        bendZ = Mathf.Clamp(bendZ, currentMinBendZ, currentMaxBendZ);
        //Debug.Log("BEND:" + move + ", " + angleOf + ";___ " + angle + ", " + xComponent + "," + zComponent + ", " + bendX + ":" + bendZ);
    }

    public void Reset()
    {
        bendX = 0;
        bendZ = 0;
        transform.rotation = Quaternion.Euler(bendX, 0, bendZ);
    }



    void OnCollisionEnter(Collision collision)
    {
        Disable();
    }
}
