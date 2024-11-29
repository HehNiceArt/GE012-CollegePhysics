using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunLoader : MonoBehaviour
{
    public Toggle toggle;
    private void Start()
    {
        LoadToggleState();
    }
    void LoadToggleState()
    {
        if (PlayerPrefs.HasKey("ToggleState"))
        {
            toggle.isOn = PlayerPrefs.GetInt("ToggleState") == 1;
        }
    }
}
