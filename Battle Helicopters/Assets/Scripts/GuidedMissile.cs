using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuidedMissile : Missile
{
    public float visionConeAngle = 45f;
    public float turnSpeed = 5f;
    public float detectionRange = 15f;

    public Transform target;
    public bool isLocked = false;

    protected virtual void Update()
    {
        if (!isLocked)
        {
            SearchForTarget();
        }

        else if (target != null)
        {
            ApplyGuidance();
        }

        else
        {
            isLocked = false;
        }
    }

    private void SearchForTarget()
    {
        Collider2D[] potentialTargets = Physics2D.OverlapCircleAll(transform.position, detectionRange, damageLayers);
        float closestDistance = Mathf.Infinity;

        foreach (var col in potentialTargets)
        {
            // The child class defines IF this specific collider is a valid target
            if (IsValidTarget(col))
            {
                Vector2 dirToTarget = (col.transform.position - transform.position).normalized;
                float dot = Vector2.Dot(transform.right, dirToTarget);
                float angleThreshold = Mathf.Cos(visionConeAngle * Mathf.Deg2Rad);

                if (dot >= angleThreshold)
                {
                    float dist = Vector2.Distance(transform.position, col.transform.position);
                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        target = col.transform;
                        isLocked = true;
                    }
                }
            }
        }
    }

    private void ApplyGuidance()
    {
        Vector2 direction = (Vector2)target.position - rb.position;
        direction.Normalize();

        // Cross product tells us if we need to turn Left or Right to face the target
        float rotateAmount = Vector3.Cross(direction, transform.right).z;
        rb.angularVelocity = -rotateAmount * turnSpeed * 100f;

        // Keep moving forward at constant speed
        rb.velocity = transform.right * speed;
    }

    // Every child (Player/Enemy) MUST implement this to decide who to hit
    protected abstract bool IsValidTarget(Collider2D other);

    private void OnDrawGizmos()
    {
        // 1. Draw the Detection Radius
        // Yellow if searching, Red if locked onto a target
        Gizmos.color = isLocked ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // 2. Draw the Vision Cone (Field of View)
        // We use transform.right because your code uses it as the forward vector
        Gizmos.color = Color.cyan;

        // Calculate the boundary vectors for the vision cone
        Vector3 leftBoundary = Quaternion.Euler(0, 0, visionConeAngle) * transform.right;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, -visionConeAngle) * transform.right;

        // Draw the boundary lines
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRange);

        // 3. Draw a line to the target if one is locked
        if (isLocked && target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);

            // Optional: Draw a small cube at the target's position for clarity
            Gizmos.DrawWireCube(target.position, Vector3.one * 0.5f);
        }
    }
}
