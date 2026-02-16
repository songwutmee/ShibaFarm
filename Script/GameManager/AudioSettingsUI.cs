using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Mixer & Sliders")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MASTER_KEY = "Vol_Master";
    private const string MUSIC_KEY = "Vol_Music";
    private const string SFX_KEY = "Vol_SFX";

    private const string MASTER_PARAM = "MasterVolume";
    private const string MUSIC_PARAM = "MusicVolume";
    private const string SFX_PARAM = "SFXVolume";

    private void Start()
    {
        if (masterSlider != null) masterSlider.minValue = 0f;
        if (masterSlider != null) masterSlider.maxValue = 1f;
        if (musicSlider != null) musicSlider.minValue = 0f;
        if (musicSlider != null) musicSlider.maxValue = 1f;
        if (sfxSlider != null) sfxSlider.minValue = 0f;
        if (sfxSlider != null) sfxSlider.maxValue = 1f;

        float master = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float music = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfx = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        if (masterSlider != null) masterSlider.value = master;
        if (musicSlider != null) musicSlider.value = music;
        if (sfxSlider != null) sfxSlider.value = sfx;

        ApplyMaster(master);
        ApplyMusic(music);
        ApplySFX(sfx);
    }

    public void OnMasterSliderChanged(float value)
    {
        ApplyMaster(value);
    }

    public void OnMusicSliderChanged(float value)
    {
        ApplyMusic(value);
    }

    public void OnSFXSliderChanged(float value)
    {
        ApplySFX(value);
    }

    private void ApplyMaster(float v)
    {
        PlayerPrefs.SetFloat(MASTER_KEY, v);
        mixer.SetFloat(MASTER_PARAM, SliderToDb(v));
    }

    private void ApplyMusic(float v)
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, v);
        mixer.SetFloat(MUSIC_PARAM, SliderToDb(v));
    }

    private void ApplySFX(float v)
    {
        PlayerPrefs.SetFloat(SFX_KEY, v);
        mixer.SetFloat(SFX_PARAM, SliderToDb(v));
    }

    private float SliderToDb(float v)
    {
        if (v <= 0.0001f)
            return -80f;    
        return Mathf.Log10(v) * 20f;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
