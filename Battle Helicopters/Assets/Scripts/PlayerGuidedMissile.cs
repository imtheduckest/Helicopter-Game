using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuidedMissile : GuidedMissile
{
    protected override void SetInitialVelocity()
    {
        rb.velocity = transform.right * speed;
    }

    protected override bool IsValidTarget(Collider2D other)
    {
        return other.CompareTag("Enemy");
    }
}
