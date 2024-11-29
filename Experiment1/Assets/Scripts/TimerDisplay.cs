using System.Collections;
using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI depthText;
    public float duration = 3.41f;
    public float targetDepth = 57.0f;
    private float currentTime = 0f;
    private float currentDepth = 0f;

    void Start()
    {
        timerText.text = "0.00s";
        depthText.text = "0.00m";
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime; // Increment the time based on real time

            // Calculate the current depth as the timer progresses
            currentDepth = Mathf.Lerp(0f, targetDepth, currentTime / duration);
            timerText.text = currentTime.ToString("F2") + "s";

            // Update the depth text with the current depth (rounded to 2 decimal places)
            depthText.text = currentDepth.ToString("F2") + "m";

            yield return null; // Wait until the next frame
        }

        // Ensure the timer stops exactly at the duration
        currentTime = duration;
        currentDepth = targetDepth;

        timerText.text = currentTime.ToString("F2") + "s";
        depthText.text = currentDepth.ToString("F2") + "m";

    }
}
