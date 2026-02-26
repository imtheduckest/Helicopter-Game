using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMissiles : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float nextFireTime;

    public void FireMissile()
    {
        if (Time.time < nextFireTime) return;

        Instantiate(
            missilePrefab,
            firePoint.position,
            firePoint.rotation
        );  

        nextFireTime = Time.time + fireRate;
    }
}
