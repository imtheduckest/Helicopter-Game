using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossSystem : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject panelWin;
    public GameObject panelLoss;

    private HelicopterHealth playerHealth;
    private bool isGameFinished = false;

    void Start()
    {
        // Ищем вертолет игрока на сцене
        playerHealth = FindObjectOfType<HelicopterHealth>();

        if (playerHealth != null)
        {
            // Подписываемся на событие смерти вертолета
            playerHealth.OnDeathEvent += OnPlayerRoomDestroyed;
        }

        // Прячем панели в начале игры
        if (panelWin != null) panelWin.SetActive(false);
        if (panelLoss != null) panelLoss.SetActive(false);
    }

    private void OnDestroy()
    {
        // Обязательно отписываемся при уничтожении объекта, чтобы не было утечек памяти
        if (playerHealth != null)
        {
            playerHealth.OnDeathEvent -= OnPlayerRoomDestroyed;
        }
    }

    private void OnPlayerRoomDestroyed()
    {
        if (isGameFinished) return;

        isGameFinished = true;

        if (panelLoss != null) panelLoss.SetActive(true);
        Time.timeScale = 0f;
    }

    public void WinGame()
    {
        if (isGameFinished) return;

        isGameFinished = true;

        if (panelWin != null) panelWin.SetActive(true);
        Time.timeScale = 0f;
    }
}
