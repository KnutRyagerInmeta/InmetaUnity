using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBall : MonoBehaviour
{

    [SerializeField] float power = 15;
    [SerializeField] float chargeSpeed = 0.5f;
    [SerializeField] float maxCharge = 3;
    [SerializeField] float currentCharge;
    [SerializeField] float energyDrainPerSecond = 1;
    [SerializeField] Rigidbody target;
    private bool currentlyCharging;
    private int lastChargeFrame;
    private BoostBallState currentState = BoostBallState.Idle;
    private Battery battery;
    private int frameAcceptedLag = 3;

    // Use this for initialization
    void Start()
    {
        target = GetComponentInParent<Rigidbody>();
        battery = target.GetComponentInChildren<Battery>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case BoostBallState.Idle:
                //Debug.Log("Idle: " + lastChargeFrame + " ?= " + Time.frameCount);
                if (lastChargeFrame >= Time.frameCount - frameAcceptedLag)
                {
                    currentState = BoostBallState.Charging;
                }

                break;
            case BoostBallState.Charging:
                //Debug.Log("Charging: " + currentCharge + ", " + lastChargeFrame + " ?= " + Time.frameCount);
                if (currentCharge < maxCharge)
                {
                    if (battery.Drain(energyDrainPerSecond * Time.fixedDeltaTime))
                    {
                        currentCharge += chargeSpeed * Time.fixedDeltaTime;
                        currentCharge = Mathf.Min(currentCharge, maxCharge);
                        if (lastChargeFrame < Time.frameCount - frameAcceptedLag)
                        {
                            currentState = BoostBallState.Released;
                        }
                    }
                    else
                    {
                        currentState = BoostBallState.Released;
                    }
                }
                break;
            case BoostBallState.Released:
                //Debug.Log("Release: " + currentCharge);
                currentCharge -= Time.fixedDeltaTime;
                var acceleration = target.velocity.normalized * power;
                acceleration.y = 0;
                target.AddForce(acceleration, ForceMode.Acceleration);
                if (currentCharge <= 0)
                {
                    currentState = BoostBallState.Idle;
                }
                break;
        }
    }

    public void Charge()
    {
        if (currentState == BoostBallState.Charging || currentState == BoostBallState.Idle)
        {
            lastChargeFrame = Time.frameCount;

        }
    }

    private enum BoostBallState
    {
        Idle, Charging, Released
    }
}
