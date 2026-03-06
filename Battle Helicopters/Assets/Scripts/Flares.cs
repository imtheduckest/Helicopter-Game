using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static UnityEngine.Rendering.DebugUI;

public class Flares : MonoBehaviour
{
    public GameObject flares;
    private bool isPaused = false;

    public VisualEffect vfx;

    void Update()
    {
        vfx.SetVector3("Spawn Pos", transform.position);
    }

    public void LaunchFlares()
    {
        isPaused = !isPaused;

        if (isPaused == true)
        {
            flares.SetActive(isPaused);
        }

        else
        {
            flares.SetActive(false);
        }
    }
}
