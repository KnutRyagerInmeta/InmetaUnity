using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShield : MonoBehaviour
{
    [SerializeField] float energyDrainSpeed;
    [SerializeField] PowerStorage powerSource;
    [SerializeField] bool isActive = false;
    [SerializeField] GameObject target;
    [SerializeField] Health health;

    void Start()
    {
        health = target.gameObject.GetComponentInChildren<Health>();
        powerSource = target.gameObject.GetComponentInChildren<PowerStorage>();
    }

    void Update()
    {
        if (isActive)
        {
            if (energyDrainSpeed > 0 && (powerSource == null || !powerSource.Drain(energyDrainSpeed * Time.deltaTime)))
            {
                Deactivate();
            }
        }
    }

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            health.invulnerable++;
            gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        if (isActive)
        {
            isActive = false;
            health.invulnerable--;
            gameObject.SetActive(false);
        }
    }

    internal void Toggle()
    {
        if (!isActive)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }
}
