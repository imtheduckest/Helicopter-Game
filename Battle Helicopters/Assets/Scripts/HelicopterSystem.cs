using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSystem : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    public float liftForce = 12f;
    public float descendForce = 10f;
    public float moveForce = 8f;
    public float downForce = 10f;

    [Header("Tilt")]
    public float maxTiltAngle = 25f;
    public float tiltSpeed = 5f;

    [Header("Stability")]
    public float hoverForce = 9.8f;
    public float fallSpeed = 2f;

    [Header("Rotors")]
    private float maxMainRotorSpeed = 2000f;
    private float maxTailRotorSpeed = 1000f;

    private float currentMainRotorSpeed;
    private float currentTailRotorSpeed;

    public Transform mainRotor;
    public Transform tailRotor;

    public JoystickMovement joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (joystick == null) return;

        float vertical = joystick.joystick_vector.y;
        float horizontal = joystick.joystick_vector.x;

        // Rotors
        mainRotor.Rotate(0, currentMainRotorSpeed * Time.deltaTime, 0);
        tailRotor.Rotate(0, 0, currentTailRotorSpeed * Time.deltaTime);

        // Hover
        rb.AddForce(transform.up * hoverForce, ForceMode2D.Force);

        // Up
        if (vertical > 0f)
        {
            rb.AddForce(transform.up * liftForce * vertical, ForceMode2D.Force);
            rb.AddForce(-transform.up * downForce * Mathf.Abs(vertical), ForceMode2D.Force);

            currentMainRotorSpeed = Mathf.Lerp(currentMainRotorSpeed, maxMainRotorSpeed, Time.deltaTime);
            currentTailRotorSpeed = Mathf.Lerp(currentTailRotorSpeed, maxTailRotorSpeed, Time.deltaTime);
        }
        // Down
        else if (vertical < -0.4f)
        {
            rb.AddForce(-transform.up * (fallSpeed + 1f), ForceMode2D.Force);

            currentMainRotorSpeed = Mathf.Lerp(currentMainRotorSpeed, maxMainRotorSpeed / 2, Time.deltaTime);
            currentTailRotorSpeed = Mathf.Lerp(currentTailRotorSpeed, maxTailRotorSpeed / 2, Time.deltaTime);
        }
        // Free fall
        else
        {
            rb.AddForce(-transform.up * fallSpeed, ForceMode2D.Force);

            currentMainRotorSpeed = Mathf.Lerp(currentMainRotorSpeed, 0, Time.deltaTime);
            currentTailRotorSpeed = Mathf.Lerp(currentTailRotorSpeed, 0, Time.deltaTime);
        }

        // Horizontal movement
        rb.AddForce(Vector2.right * horizontal * moveForce, ForceMode2D.Force);

        // Tilt
        float targetTilt = -horizontal * maxTiltAngle;

        float newAngle = Mathf.LerpAngle(rb.rotation, targetTilt, tiltSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);
    }
}
