using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject settingsPanel;

    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string bgPref = "BGPref";
    private static readonly string sfxPref = "SFXPref";

    private int firstPlayInt;

    public Slider bgSlider, sfxSlider;
    private float bgFloat, sfxFloat;

    public AudioSource bgAudio;
    [SerializeField] private AudioSource[] sfxAudio;


    public void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay, 0);
        if (firstPlayInt == 0)
        {
            bgFloat = .25f;
            sfxFloat = .75f;
            bgSlider.value = bgFloat;
            sfxSlider.value = sfxFloat;

            PlayerPrefs.SetFloat(bgPref, bgFloat);
            PlayerPrefs.SetFloat(sfxPref, sfxFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            bgFloat = PlayerPrefs.GetFloat(bgPref, 0.25f);
            bgSlider.value = bgFloat;
            sfxFloat = PlayerPrefs.GetFloat(sfxPref, 0.75f);
            sfxSlider.value = sfxFloat;
        }

        settingsPanel.SetActive(false);

        ApplySavedSettings();
    }

    public void ApplySavedSettings()
    {
        if (bgAudio != null)
        {
            bgAudio.volume = bgSlider.value;
        }

        foreach (var sfx in sfxAudio)
        {
            if (sfx != null)
            {
                sfx.volume = sfxSlider.value;
            }
        }
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat(bgPref, bgSlider.value);
        PlayerPrefs.SetFloat(sfxPref, sfxSlider.value);
        PlayerPrefs.Save();
    }

    void OnApplicationFocus(bool inFocus)
    {
        if (!inFocus)
        {
            SaveSound();
        }
    }

    public void UpdateSound()
    {
        if (bgAudio != null && bgSlider != null)
        {
            bgAudio.volume = bgSlider.value;
            SaveSound();
        }
        else
        {
        }

        if (sfxAudio != null)
        {
            foreach (var sfx in sfxAudio)
            {
                if (sfx != null)
                {
                    sfx.volume = sfxSlider.value;
                    SaveSound();
                }
                else
                {
                    // hahahahhahahahahahhahahahahaha
                }
            }
        }
        else
        {
        }
    }

    #region

    public void PlayGame()
    {
        SaveSound();
        SceneManager.LoadScene("TutorialStory");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    #endregion
}
