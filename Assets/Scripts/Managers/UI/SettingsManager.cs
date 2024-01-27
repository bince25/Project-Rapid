using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI; // For handling audio settings

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    public TMP_Dropdown resolutionDropdown;

    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (Resolution resolution in Screen.resolutions)
        {
            string option = resolution.width + " x " + resolution.height;
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.onValueChanged.AddListener(delegate { ChangeResolution(resolutionDropdown.value); });

        LoadSettings();

        _masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        _musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        AudioManager.Instance.PlayMusic(MusicTrack.BackgroundMusic, true, 0.4f);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", _masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolumeSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", _sfxVolumeSlider.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        _masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0);
        _musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);
        _sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);

        SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume", 0));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 0);
    }

    void ChangeResolution(int index)
    {
        Resolution selectedResolution = Screen.resolutions[index];
        SetResolution(selectedResolution.width, selectedResolution.height);
    }


    public void SetVolume(float volume)
    {
        AudioManager.Instance.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); // Convert volume to dB
    }

    public void SetMasterVolume(float volume)
    {
        // Ensure volume is never 0 to avoid negative infinity in logarithmic conversion
        float normalizedVolume = Mathf.Clamp(volume, 0.0001f, 5f) / 5f; // Normalize to 0-1 range
        AudioManager.Instance.audioMixer.SetFloat("MasterVolume", Mathf.Log10(normalizedVolume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        float normalizedVolume = Mathf.Clamp(volume, 0.0001f, 5f) / 5f; // Normalize to 0-1 range
        AudioManager.Instance.audioMixer.SetFloat("MusicVolume", Mathf.Log10(normalizedVolume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        float normalizedVolume = Mathf.Clamp(volume, 0.0001f, 5f) / 5f; // Normalize to 0-1 range
        AudioManager.Instance.audioMixer.SetFloat("SFXVolume", Mathf.Log10(normalizedVolume) * 20);
    }

    public void SetResolution(int width, int height)
    {
        Screen.SetResolution(width, height, Screen.fullScreen);
    }
}
