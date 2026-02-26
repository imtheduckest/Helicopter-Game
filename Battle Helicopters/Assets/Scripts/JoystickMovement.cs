using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickMovement : MonoBehaviour
{
    public RectTransform joystick;
    public RectTransform joystick_bg;
    public Canvas canvas;

    public Vector2 joystick_vector;

    private Vector2 joystick_center;

    private float joystick_radius;

    void Start()
    {
        joystick_radius = joystick_bg.sizeDelta.x / 2f;
    }

    public void PointerDown(BaseEventData data)
    {
        Drag(data);
    }

    public void Drag(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        Vector2 localPoint;

        // переводим экранные координаты в локальные координаты джойстика
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystick_bg,
            pointerData.position,
            canvas.worldCamera,
            out localPoint))
        {
            // ограничиваем радиус
            Vector2 clamped = Vector2.ClampMagnitude(localPoint, joystick_radius);

            joystick.anchoredPosition = clamped;

            joystick_vector = clamped / joystick_radius;
        }
    }

    public void PointerUp(BaseEventData data)
    {
        joystick_vector = Vector2.zero;
        joystick.anchoredPosition = Vector2.zero;
    }
}
