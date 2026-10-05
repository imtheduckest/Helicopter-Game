using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionForce = 10f;
    [SerializeField] private float torqueForce = 200f;

    [Header("Lifetime Settings")]
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float fadeDuration = 1f;

    private SpriteRenderer[] childRenderers;

    private void Start()
    {
        childRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Находим все дочерние куски с Rigidbody2D и толкаем их от центра
        Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();

        foreach (Rigidbody2D rb in rbs)
        {
            // Случайный вектор направления разлёта
            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            // Применяем импульсную силу и вращение
            rb.AddForce(randomDirection * explosionForce, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-torqueForce, torqueForce));
        }

        // Запускаем таймер удаления
        StartCoroutine(FadeAndDestroyRoutine());
    }

    private IEnumerator FadeAndDestroyRoutine()
    {
        yield return new WaitForSeconds(lifetime);

        float elapsed = 0f;

        // Плавное исчезновение через прозрачность (Alpha)
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            foreach (var sr in childRenderers)
            {
                if (sr != null)
                {
                    Color color = sr.color;
                    color.a = newAlpha;
                    sr.color = color;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
