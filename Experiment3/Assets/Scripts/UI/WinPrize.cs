using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPrize : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    [SerializeField] SpeedrunTimer speedrunTimer;
    [SerializeField] float delayPopUp = 5f;

    private void Start()
    {
        winUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PEShirt"))
        {
            StartCoroutine(DelayWinUI());
        }
    }
    IEnumerator DelayWinUI()
    {
        yield return new WaitForSeconds(delayPopUp);
        winUI.SetActive(true);
        speedrunTimer.StopTimer();
        Time.timeScale = 0;
    }
}
