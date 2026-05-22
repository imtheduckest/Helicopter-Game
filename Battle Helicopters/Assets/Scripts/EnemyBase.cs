using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour , IDamageable
{
    public float maxHealth = 100;
    protected float currentHealth;

    public Sprite deadSprite;

    protected SpriteRenderer spriteRenderer;
    protected bool isDead;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        Debug.Log("Taken Damage");
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        isDead = true;

        if (deadSprite != null)
            spriteRenderer.sprite = deadSprite;

        this.enabled = false;

        OnDeath();
    }

    // Hook for subclasses
    protected virtual void OnDeath() { }
}
