using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideSettings : MonoBehaviour
{
    // ----------------------------------------
    // REFERENCES
    // ----------------------------------------
    [Header("References")]
    public CanvasGroup mainMenuGroup;
    public CanvasGroup settingsGroup;
    public float fadeTime = 0.3f;

    [Header("Volume")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    // ----------------------------------------
    // UNITY EVENTS
    // ----------------------------------------
    private void Start()
    {
        // Initialize settings, credits, bugs panels as hidden and non-interactable
        if (settingsGroup != null)
        {
            settingsGroup.alpha = 0;
            settingsGroup.interactable = false;
            settingsGroup.blocksRaycasts = false;
        }
    }

    // ----------------------------------------
    // PANEL CONTROLS
    // ----------------------------------------

    public void ShowSettings()
    {
        if (settingsGroup == null || mainMenuGroup == null) return;

        // Show settings panel, disable main menu interaction
        settingsGroup.alpha = 1;
        settingsGroup.interactable = true;
        settingsGroup.blocksRaycasts = true;

        mainMenuGroup.interactable = false;
        SyncSliderWithValue();
    }

    public void HideSettings()
    {
        if (settingsGroup == null || mainMenuGroup == null) return;

        // Hide settings panel, re-enable main menu interaction
        settingsGroup.alpha = 0;
        settingsGroup.interactable = false;
        settingsGroup.blocksRaycasts = false;

        mainMenuGroup.interactable = true;
    }

    // ----------------------------------------
    // VOLUME SETTINGS
    // ----------------------------------------
    public void SetVolume()
    {
        if (bgmSlider == null) return;
        if (sfxSlider == null) return;

        SoundManager.Instance.SetMusicVolume(bgmSlider.value);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value);
    }

    public void SyncSliderWithValue()
    {
        if (bgmSlider == null || sfxSlider == null || SoundManager.Instance == null)
            return;

        bgmSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
}
