using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : Missile
{
    protected override void SetInitialVelocity()
    {
        rb.velocity = -transform.right.normalized * speed;
    }
}
