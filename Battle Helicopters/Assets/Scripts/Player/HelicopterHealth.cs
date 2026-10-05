using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Armor Settings")]
    [Range(0f, 1f)]
    public float armorReduction = 0f; // 0 = нет брони, 0.3 = поглощает 30% урона, 1 = полный иммунитет

    private bool isDead = false;

    public event Action OnDeathEvent;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        // Рассчитываем урон с учетом брони
        // Пример: если armorReduction = 0.2 (20%), игрок получит 80% урона
        float finalDamage = amount * (1f - armorReduction);

        // Округляем или защищаем от отрицательного урона (на случай странных расчетов)
        finalDamage = Mathf.Max(finalDamage, 0f);

        currentHealth -= finalDamage;
        Debug.Log($"Вертолет получил {finalDamage} урона (С учетом брони). Осталось HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Вертолет уничтожен!");

        // 1. Отключаем управление вертолетом, чтобы он не летал после смерти
        if (TryGetComponent<HelicopterSystem>(out HelicopterSystem movement))
        {
            movement.enabled = false;
        }

        // 2. Здесь включаешь анимацию взрыва, падение или экран Game Over
        // Например, можно врубить гравитацию на максимум, чтобы он реалистично упал
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.constraints = RigidbodyConstraints2D.None; // Разрешаем хаотично крутиться при падении
        }

        OnDeathEvent?.Invoke();
    }
}
