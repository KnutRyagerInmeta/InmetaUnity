using UnityEngine;

public class Explosive : MonoBehaviour
{

    [SerializeField] float power;
    [SerializeField] float radius;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] Audio audio;
    [SerializeField] Renderer renderer;

    // Use this for initialization
    void Start()
    {
        //particleSystem = GetComponentInChildren<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Explode()
    {

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        //Debug.Log("EXPLODE0: " + explosionPos + "," + radius + ": " + colliders.Length);
        if (particleSystem != null)
        {
            particleSystem.gameObject.SetActive(true);
            particleSystem.Play();
            Destroy(gameObject, particleSystem.main.duration);
            var body = gameObject.GetComponent<Rigidbody>();
            if (body != null)
            {
                body.isKinematic = true;
            }
            if (renderer == null)
            {
                renderer = gameObject.GetComponentInChildren<Renderer>();
            }
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        else
        {
            Destroy(gameObject, 0);
        }
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponentInChildren<Rigidbody>();
            if (rb == null)
            {
                rb = hit.GetComponentInParent<Rigidbody>();
            }
            var dist = Vector3.Distance(hit.ClosestPoint(transform.position), transform.position);
            var damage = power * 0.1f / dist;
            //Debug.Log("EXPLODE: " + rb.name + "," + power);
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 0F);
                var health = hit.GetComponentInChildren<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
            var block = hit.GetComponent<Block>();
            if (block != null)
            {
                block.ExplodeOn(damage);
            }
            var weakPoint = hit.GetComponent<WeakPoint>();
            if(weakPoint != null)
            {
                weakPoint.Trigger();
            }
            //Debug.Log("HIT: " + hit.gameObject.name + ", "+ (weakPoint != null));
        }
        if (audio != null)
        {
            audio.Play();
        }
    }
}
