using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyTank : EnemyBase
{
    [Header("References")]
    public Transform turretTransform;
    public GameObject projectilePrefab;
    public Transform firePoint;
    private Transform player;

    [Header("Settings")]
    public float detectionRadius = 10f;
    public float rotationSpeed = 2f;
    public float shootInterval = 5f;

    private float timer;

    protected override void Awake()
    {
        base.Awake(); // This runs the code inside EnemyBase.Awake() first

        GameObject playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            RotateTurret();

            // Vector check timer
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                CheckAndShoot();
                timer = 0;
                Debug.Log("Vector is pointing at you");
            }
        }
    }

    void RotateTurret()
    {
        // Calculate direction to player
        Vector2 direction = (Vector2)player.position - (Vector2)turretTransform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        // Slowly rotate toward that angle (assuming your turret sprite faces Right/East)
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        turretTransform.rotation = Quaternion.Lerp(turretTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void CheckAndShoot()
    {
        // We check if the Turret's Right vector (forward for 2D) is pointing at the player
        Vector2 directionToPlayer = (player.position - turretTransform.position).normalized;
        float dotProduct = Vector2.Dot(-turretTransform.right, directionToPlayer);
        Debug.Log(dotProduct);
        // If dotProduct is close to 1, the vectors are pointing in the same direction
        if (dotProduct > 0.7f)
        {
            Instantiate(projectilePrefab, firePoint.position, turretTransform.rotation);
        }
    }

    // Visual aid in the Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    protected override void OnDeath()
    {

    }
}
