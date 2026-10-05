using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Barrel : EnemyBase
{
    [Header("Debris Settings")]
    [SerializeField] private GameObject debrisPrefab; // Префаб с кусками бочки

    protected override void OnDeath()
    {
        // 1. Спавним префаб с осколками на месте бочки
        if (debrisPrefab != null)
        {
            Instantiate(debrisPrefab, transform.position, transform.rotation);
        }

        // 2. Уничтожаем основной объект бочки
        Destroy(gameObject);
    }
}
