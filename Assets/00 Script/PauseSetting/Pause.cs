using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider, _sfxSlider;
    [SerializeField] GameObject _pausePanel;
    private void Start()
    {
        _pausePanel.SetActive(false);
        _volumeSlider.value = PlayerPrefsController.GetPlayerVolume();
        _sfxSlider.value = PlayerPrefsController.GetPlayerSFX();
    }
    private void Update()
    {
        SoundVolume();
        SFXVolume();
    }
    public void PauseGame()
    {
        _pausePanel.gameObject.SetActive(true);
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetPlayerVolume(_volumeSlider.value);
        PlayerPrefsController.SetPlayerSFX(_sfxSlider.value);
        _pausePanel.gameObject.SetActive(false);
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    public void SoundVolume()
    {
        MusicPlayer.Instant.SetSoundVolume(_volumeSlider.value);
    }
    public void SFXVolume()
    {
        MusicPlayer.Instant.SetSFXVolume(_sfxSlider.value);
    }
    public void MuteSound()
    {
        MusicPlayer.Instant.MuteSound();
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    public void MuteSFX()
    {
        MusicPlayer.Instant.PlaySFX("Button_Sound");
        MusicPlayer.Instant.MuteSFX();
    }
}
