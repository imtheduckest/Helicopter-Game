using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterGun : MonoBehaviour
{
    [SerializeField] private float range = 20;
    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float fireRate = 0.1f;
    private float nextFireTime;

    public void Fire()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        Vector3 direction = firePoint.right;

        var hit = Physics2D.Raycast(
            firePoint.position,
            direction,
            range
        );

        Debug.DrawRay(firePoint.position, direction * range, Color.red, 0.1f);

        Vector3 endPosition;

        if (hit.collider != null)
        {
            endPosition = hit.point;

            var idamageable = hit.collider.GetComponent<IDamageable>();
            idamageable?.TakeDamage(10);
        }
        else
        {
            endPosition = firePoint.position + direction * range;
        }

        var laser = Instantiate(laserPrefab);
        laser.GetComponent<BulletTrail>().Init(
            firePoint.position.WithAxis(Axis.Z, -1),
            endPosition.WithAxis(Axis.Z, -1)
        );
    }

}
