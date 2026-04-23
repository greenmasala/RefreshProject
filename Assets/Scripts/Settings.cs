using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings: MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    public Slider MasterSlider;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public TextMeshProUGUI MasterVolVal;
    public TextMeshProUGUI MusicVolVal;
    public TextMeshProUGUI SFXVolVal;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVol") || PlayerPrefs.HasKey("MusicVol") || PlayerPrefs.HasKey("SFXVol"))
        {
            LoadSettings();
            Debug.Log("Settings loaded");
        }
        else
        {
            Debug.Log("Default settings");
            SetMasterVol(0.75f);
            SetMusicVol(0.5f);
            SetSFXVol(0.5f);
        }
    }
    public void SetMasterVol(float vol)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(vol) * 20f);
        var percentage = (vol / 1) * 100;
        MasterVolVal.text = percentage.ToString("F0");
        PlayerPrefs.SetFloat("MasterVol", vol);
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVol");
    }
    public void SetMusicVol(float vol)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(vol) * 20f);
        var percentage = (vol / 1) * 100;
        MusicVolVal.text = percentage.ToString("F0");
        PlayerPrefs.SetFloat("MusicVol", vol);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVol");
    }

    public void SetSFXVol(float vol)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(vol) * 20f);
        var percentage = (vol / 1) * 100;
        SFXVolVal.text = percentage.ToString("F0");
        PlayerPrefs.SetFloat("SFXVol", vol);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
    }

    public void LoadSettings()
    {
        SetMasterVol(PlayerPrefs.GetFloat("MasterVol"));
        SetMusicVol(PlayerPrefs.GetFloat("MusicVol"));
        SetSFXVol(PlayerPrefs.GetFloat("SFXVol"));   
    }
}
