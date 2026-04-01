using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMissiles : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform firePoint;

    public float fireRate = 5f;
    public float lockTime = 5f;

    private float nextFireTime;
    private float currentLockLevel;

    void Update()
    {
        if (Time.time < nextFireTime)
        {
            currentLockLevel = 0f;
        }

        else
        {
            // Calculate progress from 0 to 1 based on waiting AFTER reload
            currentLockLevel = Mathf.Clamp01((Time.time - nextFireTime) / lockTime);
        }
    }

    public void AttemptFire()
    {
        if (Time.time >= nextFireTime)
        {
            FireMissile();
        }

        else
        {
            Debug.Log($"Missile reloading... {nextFireTime - Time.time:F1}s left");
        }
    }

    public void FireMissile()
    {
        // Instantiate the missile
        GameObject go = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);

        // Pass the lock level to the Guided script
        PlayerGuidedMissile missileScript = go.GetComponent<PlayerGuidedMissile>();

        // Apply the current accuracy to the missile
        // Min: 5° cone / 2 turn speed | Max: 60° cone / 15 turn speed
        missileScript.visionConeAngle = 5f + (currentLockLevel * 55f);
        missileScript.turnSpeed = 2f + (currentLockLevel * 13f);

        Debug.Log($"Fired! Accuracy Level: {currentLockLevel * 100}%");

        nextFireTime = Time.time + fireRate;
    }
}