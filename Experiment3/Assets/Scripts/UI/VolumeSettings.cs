using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider bgSlider;
    [SerializeField] Slider sfxSlider;

    const string MIXER_BG = "bgVolume";
    const string MIXER_SFX = "sfxVolume";
    void Awake()
    {
        bgSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_BG, Mathf.Log10(value) * 20);
    }

    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
