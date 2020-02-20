using UnityEngine;

public class ForceField : MonoBehaviour
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
        if (isActive && energyDrainSpeed > 0 && (powerSource == null || !powerSource.Drain(energyDrainSpeed * Time.deltaTime)))
        {
            Deactivate();
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

    public void Toggle()
    {
        if (isActive)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
}
