using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTick : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    private void Start()
    {
        LoadToggleState();

        toggle.onValueChanged.AddListener(delegate
        {
            SaveToggleState();
        });
    }
    void SaveToggleState()
    {
        PlayerPrefs.SetInt("ToggleState", toggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    void LoadToggleState()
    {
        if (PlayerPrefs.HasKey("ToggleState"))
        {
            toggle.isOn = PlayerPrefs.GetInt("ToggleState") == 1;
        }
    }
}