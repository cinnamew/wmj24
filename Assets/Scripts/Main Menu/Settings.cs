using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Settings : Singleton<Settings>
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider, sfxSlider, writingSpeedSlider;


    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(Globals.MUSIC_VOLUME, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(Globals.SFX_VOLUME, 0.75f);
        writingSpeedSlider.value = PlayerPrefs.GetFloat(Globals.WRITING_SPEED, 35f);

        mainMixer.SetFloat(Globals.MUSIC_VOLUME,
            20 * Mathf.Log10(musicSlider.value + float.Epsilon));
        mainMixer.SetFloat(Globals.SFX_VOLUME,
            20 * Mathf.Log10(sfxSlider.value + float.Epsilon));

        musicSlider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(Globals.MUSIC_VOLUME, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(Globals.MUSIC_VOLUME, val);
        });

        sfxSlider.onValueChanged.AddListener(val =>
        {
            mainMixer.SetFloat(Globals.SFX_VOLUME, 20 * Mathf.Log10(val + float.Epsilon));
            PlayerPrefs.SetFloat(Globals.SFX_VOLUME, val);
        });

        writingSpeedSlider.onValueChanged.AddListener(val =>
        {
            if (DialogueManager.Instance != null) DialogueManager.Instance.ChangeWritingSpeed(val);
            PlayerPrefs.SetFloat(Globals.WRITING_SPEED, val);
        });
    }
}
