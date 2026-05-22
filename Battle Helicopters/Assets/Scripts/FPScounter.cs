using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPScounter : MonoBehaviour
{
    private Text fpsText;
    public float updateInterval = 0.5f;

    private float accumulatedTime = 0f;
    private int frameCount = 0;
    private float timeLeft;

    void Start()
    {
        fpsText = GetComponent<Text>();
        timeLeft = updateInterval;
    }

    void Update()
    {
        timeLeft -= Time.unscaledDeltaTime;
        accumulatedTime += Time.unscaledDeltaTime;
        frameCount++;

        // Update the UI only when the interval is reached
        if (timeLeft <= 0.0)
        {
            float fps = frameCount / accumulatedTime;
            fpsText.text = string.Format("{0:F0}", fps);

            // Reset timers
            timeLeft = updateInterval;
            accumulatedTime = 0f;
            frameCount = 0;
        }
    }
}
