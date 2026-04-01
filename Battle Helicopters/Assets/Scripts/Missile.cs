using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{
    public float speed = 30f;
    public float damage = 50f;
    public float explosionRadius = 2f;
    public float lifetime = 5f;
    public LayerMask damageLayers;
    public GameObject explosionEffect;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        SetInitialVelocity();
        Invoke(nameof(Explode), lifetime);
    }

    protected abstract void SetInitialVelocity();

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    public void Explode()
    {
        // Damage in radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            explosionRadius,
            damageLayers
        );

        foreach (Collider2D hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        // Optional explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    // Debug radius
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
