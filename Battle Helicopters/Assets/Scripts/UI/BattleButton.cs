using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    // Это костыль ее надо убрать в будущем
    private void Start()
    {
        Button btn = GetComponent<Button>();

        // Автоматически подписываем кнопку на метод ЖИВОГО менеджера, который прилетел из меню
        if (btn != null && HangarValues.Instance != null)
        {
            btn.onClick.AddListener(HangarValues.Instance.LaunchLevel);
        }
    }
}
