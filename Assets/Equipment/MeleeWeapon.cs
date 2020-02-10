using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : FollowEquipment
{
    [SerializeField] float damage = 10f;
    [SerializeField] float swingTime = 0.5f;
    [SerializeField] MeleeWeaponState state = MeleeWeaponState.Idle;
    Rigidbody weaponBody;
    Mover mover;

    // Start is called before the first frame update
    void Start()
    {
        weaponBody = GetComponent<Rigidbody>();
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state == MeleeWeaponState.Swinging)
        {
            var target = collision.gameObject;
            var hp = target.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(damage);
            }
        }

    }

    public void Swing()
    {

    }

    public enum MeleeWeaponState
    {
        Idle, Swinging
    }
}
