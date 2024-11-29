using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerString;
    [SerializeField] GameObject speedRunGO;
    [SerializeField] Toggle toggle;

    float elapsedTime = 0f;
    public bool isRunning = false;
    private void Update()
    {
        if (isRunning && toggle.isOn)
        {
            speedRunGO.SetActive(true);
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (toggle.isOn == false)
        {
            speedRunGO.SetActive(false);
        }
    }
    void UpdateTimerDisplay()
    {
        int hours = (int)(elapsedTime / 3600f);
        int minutes = (int)((elapsedTime % 3600f) / 60f);
        int seconds = (int)(elapsedTime % 60f);
        int milliseconds = (int)((elapsedTime * 100f) % 100f);

        timerString.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);
    }
    public void StartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }
    public void StopTimer()
    {
        isRunning = false;
    }
    public void ResumeTimer()
    {
        isRunning = true;
    }
    public float GetCurrentTime()
    {
        return elapsedTime;
    }
}