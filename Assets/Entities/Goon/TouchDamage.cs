using UnityEngine;

public class TouchDamage : MonoBehaviour
{
    [SerializeField] float damagePerSecond;

    void OnCollisionStay(Collision collision)
    {
        var target = collision.gameObject;
        var hp = target.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }
}