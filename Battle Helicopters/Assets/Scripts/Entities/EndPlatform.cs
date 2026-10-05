using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatform : MonoBehaviour
{
    [Header("Settings")]
    public float timeRequiredToWin = 3f;

    private WinLossSystem winLossSystem;
    private Coroutine winCountdownCoroutine;
    private Rigidbody2D playerRb;

    void Start()
    {
        // Находим систему победы/проигрыша на сцене
        winLossSystem = FindObjectOfType<WinLossSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что в зону влетел именно игрок (у которого есть HelicopterSystem)
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && collision.GetComponent<HelicopterSystem>() != null)
        {
            playerRb = rb;
            // Запускаем корутину отсчета времени
            winCountdownCoroutine = StartCoroutine(WinCountdown());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Если игрок полностью улетел с платформы, отменяем всё
        if (collision.GetComponent<HelicopterSystem>() != null)
        {
            ResetCountdown();
            playerRb = null;
        }
    }

    private IEnumerator WinCountdown()
    {
        float timer = 0f;

        while (timer < timeRequiredToWin)
        {
            // Проверяем, двигается ли вертолет. 
            // Квадрат величины скорости (sqrMagnitude) эффективнее для процессора, чем magnitude.
            // Используем небольшую погрешность (0.05f), так как физика вертолета может слегка микро-дрожать.
            if (playerRb != null && playerRb.velocity.sqrMagnitude > 0.05f)
            {
                // Если сдвинулся — сбрасываем таймер текущей попытки обратно на 0
                timer = 0f;
            }
            else
            {
                // Если стоит на месте — прибавляем прошедшее время
                timer += Time.deltaTime;
            }

            yield return null; // Ждем до следующего кадра
        }

        // Если цикл завершился, значит игрок простоял 3 секунды неподвижно
        if (winLossSystem != null)
        {
            winLossSystem.WinGame();
        }
    }

    private void ResetCountdown()
    {
        if (winCountdownCoroutine != null)
        {
            StopCoroutine(winCountdownCoroutine);
            winCountdownCoroutine = null;
        }
    }
}
