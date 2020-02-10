using System;
using UnityEngine;

public enum DeathMode
{
    NONE, DESTROY, DEACTIVATE, DISAPPEAR, EXPLODE, RESPAWN
}

public class Health : MonoBehaviour
{
    public Action OnDeath;
    [SerializeField] public float MaxHp = 100;
    private float hp;
    public float Hp { get { return hp; } set { hp = value; if (healthBar != null) { healthBar.UpdateBar(hp, MaxHp); } } }
    [SerializeField] public float ratioArmor;
    [SerializeField] public float linearArmor;
    [SerializeField] public int invulnerable;
    [SerializeField] public SimpleHealthBar healthBar;
    public float RegenerationSpeed;
    public DeathMode DeathMode = DeathMode.NONE;

    void Start()
    {
        Hp = MaxHp;
    }

    void Update()
    {
        Heal(RegenerationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        if (invulnerable > 0)
        {
            return;
        }
        damage = Math.Max(0, (damage - linearArmor) * (1 - ratioArmor));
        Hp -= damage;
        if (Hp <= 0) Die();
    }

    public void Heal(float hp)
    {
        Hp = Mathf.Min(MaxHp, Hp + hp);
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
        //switch (onDeath)
        //{
        //    case OnDeath.DEATIVATE:
        //        var mover = GetComponentInChildren<Mover>();
        //        if (mover != null)
        //        {
        //            mover.enabled = false;
        //        }
        //        break;
        //    case OnDeath.DISAPPEAR:
        //        Destroy(gameObject);
        //        break;
        //}
    }
}
