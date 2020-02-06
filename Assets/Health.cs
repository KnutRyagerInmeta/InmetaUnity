using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathMode
{
    NONE, DESTROY, DEACTIVATE, EXPLODE
}

public class Health : MonoBehaviour
{
    public Action OnDeath;
    public float Hp;
    public float MaxHp = 100;
    public float RegenerationSpeed;
    public DeathMode DeathMode = DeathMode.NONE;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Heal(RegenerationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        Hp = Mathf.Max(0, Hp - damage);
        if (Hp <= 0) Die();
    }

    public void Heal(float hp)
    {
        Hp = Mathf.Max(0, Hp + hp);
    }

    public void Die()
    {
        OnDeath?.Invoke();
        switch (DeathMode)
        {
            case DeathMode.DESTROY:
                {
                    Destroy(gameObject);
                    break;
                }
            case DeathMode.DEACTIVATE:
                {
                   gameObject.SetActive(false);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
