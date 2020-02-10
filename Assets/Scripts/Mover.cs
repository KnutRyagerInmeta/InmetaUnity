using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{

    [SerializeField] Transform repositionTarget;
    private Rigidbody targetBody;
    [SerializeField] Vector3 amplitude;
    [SerializeField] Vector3 period = new Vector3(10, 10, 10);
    [SerializeField] Vector3 timeOffset;
    [SerializeField] Vector3 maxAcceleration = new Vector3(25, 25, 25);
    [SerializeField] MoveFunction moveX = MoveFunction.None;
    [SerializeField] MoveFunction moveY = MoveFunction.None;
    [SerializeField] MoveFunction moveZ = MoveFunction.None;
    [SerializeField] float maxSpeed = 1e6f;
    [SerializeField] float minDistance = 0;
    [SerializeField] WithTargetMove withTargetMove = WithTargetMove.On;
    private bool isMinDistance;
    float minSpeed;
    private float timeDelayed;
    private float timeCatchupSpeed = 2f;

    private float followTargetBehindDistance;

    Vector3 times;
    Vector3 initialPosition;

    private Rigidbody body;

    private Vector3 timestep;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        isMinDistance = minDistance != 0;
        minSpeed = -maxSpeed;
        timeOffset.x *= 2 * Mathf.PI * period.x;
        timeOffset.y *= 2 * Mathf.PI * period.y;
        timeOffset.z *= 2 * Mathf.PI * period.z;
        times = timeOffset;
        body = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        timestep = new Vector3(Time.fixedDeltaTime, Time.fixedDeltaTime, Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        var position = transform.position;
        var velocity = body.velocity;
        var velocityX = velocity.x;
        if (repositionTarget != null)
        {
            var distance = followTargetBehindDistance;
            var targetVelocity = default(Vector3);
            switch (withTargetMove)
            {
                case WithTargetMove.On:
                    initialPosition = repositionTarget.position;
                    break;
                case WithTargetMove.Infront:
                    targetVelocity = targetBody.velocity;
                    if (!(Mathf.Approximately(targetVelocity.x, 0) && Mathf.Approximately(targetVelocity.z, 0)))
                    {
                        targetVelocity.y = 0;
                        initialPosition = repositionTarget.position + targetVelocity.normalized * distance;
                    }
                    //Debug.Log("desire: " + initialPosition + "," + targetVelocity + "," + distance + "," + targetBody.position);
                    break;
                case WithTargetMove.Behind:
                    targetVelocity = targetBody.velocity;
                    if (!(Mathf.Approximately(targetVelocity.x, 0) && Mathf.Approximately(targetVelocity.z, 0)))
                    {
                        targetVelocity.y = 0;
                        initialPosition = repositionTarget.position - targetVelocity.normalized * distance;
                        //Debug.Log("YUP" + targetVelocity.ToString("F10") + "," + Mathf.Approximately(targetVelocity.x, 0f) + "," + Mathf.Approximately(targetVelocity.z, 0f));
                    }
                    //Debug.Log("desire: " + initialPosition + "," + targetVelocity + "," + distance + "," + targetBody.position);
                    break;
            }
        }
        Vector3 newPosition = default(Vector3);
        var velocityY = velocity.y;
        var velocityZ = velocity.z;
        newPosition.x = GetMoveValue(initialPosition.x, times.x, period.x, amplitude.x, moveX);
        newPosition.y = GetMoveValue(initialPosition.y, times.y, period.y, amplitude.y, moveY);
        newPosition.z = GetMoveValue(initialPosition.z, times.z, period.z, amplitude.z, moveZ);
        var isMinDistanceBlocked = isMinDistance && (isMinDistance ? Vector3.Distance(position, newPosition) : 0) > minDistance;
        var accX = getAccelerationX(position.x, velocityX, times.x, isMinDistanceBlocked);
        var accY = getAccelerationY(position.y, velocityY, times.y, isMinDistanceBlocked);
        var accZ = getAccelerationZ(position.z, velocityZ, times.z, isMinDistanceBlocked);
        if ((velocityX < minSpeed && accX < 0) || velocityX > maxSpeed && accX > 0)
        {
            accX = 0;
        }
        if ((velocityY < minSpeed && accY < 0) || velocityY > maxSpeed && accY > 0)
        {
            accY = 0;
        }
        if ((velocityZ < minSpeed && accZ < 0) || velocityZ > maxSpeed && accZ > 0)
        {
            accZ = 0;
        }
        //body.position = newPosition;
        var xDif = newPosition.x - transform.position.x;
        //Debug.Log("HM: " + position.x + ",desire: " + newPosition.x + ", acc: " + accX + ", vel: " + velocity.x + ", desired: " + desiredVX(times.x));
        //Debug.Log("HM: " + position.y + ",desire: " + newPosition.y + ", acc: " + accY + ", vel: " + velocity.y + ", desired: " + desiredVY(times.y));
        //Debug.Log("HM: " + position.z + ",desire: " + newPosition.z + ", acc: " + accZ + ", vel: " + velocity.z + ", desired: " + desiredVZ(times.z));
        //Debug.Log("acceleration: " + accX + ", " + accY + ", " + accZ + ", desired pos: " + desiredX(times.x) + ", " + desiredY(times.y) + ", " + desiredZ(times.z) + ", " + ", desired vel: " + desiredVX(times.x) + ", " + desiredVY(times.y) + ", " + desiredVZ(times.z));
        body.AddForce(new Vector3(accX, accY, accZ), ForceMode.Acceleration);
        if (isMinDistanceBlocked)
        {
            //var dist = isMinDistance ? Vector3.Distance(position, newPosition) : 0;
            //    Debug.Log("2 much: " + dist + "/" + minDistance + "," + position + "," + newPosition + ", f: " + new Vector3(accX, accY, accZ) + ", v: " + velocity + "/"
            //+ new Vector3(desiredVX(times.x), desiredVY(times.y), desiredVZ(times.z)));
            timeDelayed++;
            return;
        }
        times += timestep;
        if (timeDelayed > 0)
        {
            timeDelayed -= timeCatchupSpeed;
            times += timestep * timeCatchupSpeed;
        }
        //if (GetComponent<Goon>() != null)
        //{
        //    Debug.Log("hmm: " + initialPosition + "," + newPosition + "," + desiredX(times.x) + "," + desiredVX(times.x));
        //}
    }

    internal void Move(Vector3 step)
    {
        initialPosition += step;
    }

    private float getAccelerationX(float position, float velocity, float t, bool isMinDistanceBlocked)
    {
        var accMax = maxAcceleration.x;
        var posDesired = desiredX(t);
        var velDesired = isMinDistanceBlocked ? 0 : desiredVX(t);
        return MyMath.GetAcceleration(position, velocity, posDesired, velDesired, accMax, t);
    }

    private float getAccelerationY(float position, float velocity, float t, bool isMinDistanceBlocked)
    {
        var accMax = maxAcceleration.y;
        var posDesired = desiredY(t);
        var velDesired = isMinDistanceBlocked ? 0 : desiredVY(t);
        return MyMath.GetAcceleration(position, velocity, posDesired, velDesired, accMax, t);
    }

    private float getAccelerationZ(float position, float velocity, float t, bool isMinDistanceBlocked)
    {
        var accMax = maxAcceleration.z;
        var posDesired = desiredZ(t);
        var velDesired = isMinDistanceBlocked ? 0 : desiredVZ(t);
        return MyMath.GetAcceleration(position, velocity, posDesired, velDesired, accMax, t);
    }

    private float desiredX(float t)
    {
        return GetMoveValue(initialPosition.x, t, period.x, amplitude.x, moveX);
    }

    private float desiredVX(float t)
    {
        return (desiredX(t + timestep.x) - desiredX(t)) / timestep.x + (targetBody != null ? targetBody.velocity.x : 0);
    }

    private float desiredY(float t)
    {
        return GetMoveValue(initialPosition.y, t, period.y, amplitude.y, moveY);
    }

    private float desiredVY(float t)
    {
        return (desiredY(t + timestep.y) - desiredY(t)) / timestep.y + (targetBody != null ? targetBody.velocity.y : 0);
    }

    private float desiredZ(float t)
    {
        return GetMoveValue(initialPosition.z, t, period.z, amplitude.z, moveZ);
    }

    private float desiredVZ(float t)
    {
        return (desiredZ(t + timestep.z) - desiredZ(t)) / timestep.z + (targetBody != null ? targetBody.velocity.z : 0);
    }

    private float desiredPos(float initialPosition, float period, float amplitude, float t, MoveFunction move)
    {
        return GetMoveValue(initialPosition, t, period, amplitude, move);
    }

    private float desiredV(float initialPosition, float period, float amplitude, float t, float timestep, MoveFunction move, float targetBodyVelocity)
    {
        return (desiredPos(initialPosition, period, amplitude, t + timestep, move)
            - desiredPos(initialPosition, period, amplitude, t, move))
            / timestep + targetBodyVelocity;
    }

    private float GetMoveValue(float initial, float timestep, float period, float amplitude, MoveFunction mode)
    {
        switch (mode)
        {
            case MoveFunction.Sin:
                return initial + amplitude * Mathf.Sin(2 * Mathf.PI * timestep / period);
            case MoveFunction.Cos:
                return initial + amplitude * Mathf.Cos(2 * Mathf.PI * timestep / period);
            case MoveFunction.Swap:
                var timeMod0 = timestep % period;
                var timeRatio0 = timeMod0 / period;
                //Debug.Log("timeratio: " + timeRatio0 + ", " + (initial + (timeRatio0 < 0.5f ? amplitude : -amplitude)) + "(" + timestep + ")");
                return initial + (timeRatio0 < 0.5f ? amplitude : -amplitude);
            case MoveFunction.Linear:
                var timeMod = timestep % period;
                var timeRatio = timeMod / period;
                return initial + 2 * amplitude * (timeRatio < 0.5f ? timeRatio : 1 - timeRatio);
        }
        return initial;
    }

    public void SetTarget(Transform target)
    {
        repositionTarget = target;
        targetBody = target.GetComponent<Rigidbody>();
        switch (withTargetMove)
        {
            case WithTargetMove.Behind:
                followTargetBehindDistance = Vector3.Distance(transform.position, target.position);
                break;
        }
    }

    public enum MoveFunction
    {
        None,
        Sin,
        Cos,
        Swap,
        Linear
    }

    public enum WithTargetMove
    {
        On, Infront, Behind
    }
}
