using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class Flares : MonoBehaviour
{
    private bool isPaused = false;

    public VisualEffect vfx;

    void Update()
    {
        vfx.SetVector3("Spawn Pos", transform.position);
    }

    public void LaunchFlares()
    {
        isPaused = !isPaused;
    
        vfx.SetBool("isSpawning", isPaused);
    }
}
