using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Rigidbody2D targetRB;
    private CinemachineVirtualCamera vcam;

    public float speedThreshold = 5f; // порог входа

    public float minZoom = 5f; 
    public float maxZoom = 10f;

    public float zoomSpeed = 0.2f;
    private float maxSpeed = 10f;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float speed = targetRB.velocity.magnitude;

        float t = speed / maxSpeed;
        t = Mathf.Clamp01(t);

        float targetZoom = Mathf.Lerp(minZoom, maxZoom, t);

        vcam.m_Lens.OrthographicSize = Mathf.Lerp(
            vcam.m_Lens.OrthographicSize,
            targetZoom,
            Time.deltaTime * zoomSpeed
        );
    }
}
