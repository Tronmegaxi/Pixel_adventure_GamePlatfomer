using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public Sound[] _musicSound, _sfxSound;
    public AudioSource _musicSource, _sfxSource;

    #region SINGLETON
    private static MusicPlayer _instant;
    public static MusicPlayer Instant { get => _instant; }
    private void Awake()
    {
        if (_instant == null) { _instant = this; }
        if (_instant.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
        {
            Debug.Log(this.gameObject.name + "has been destroy");
            Destroy(this.gameObject);
        }
    }
    #endregion


    private void Start()
    {
        DontDestroyOnLoad(this);
        _musicSource.volume = PlayerPrefsController.GetPlayerVolume();
        PlayMusic("Theme");
        SetSoundVolume(0.4f);
    }
    public void SetSoundVolume(float Volume)
    {
        _musicSource.volume = Volume;
    }
    public void SetSFXVolume(float Volume)
    {
        _sfxSource.volume = Volume;
    }
    public void PlayMusic(string name)
    {
        Sound S = Array.Find(_musicSound, x => x._name == name);
        if (S == null) { Debug.LogError("MusicSound not found"); }
        else
        {
            _musicSource.clip = S._audioClipsound;
            _musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound S = Array.Find(_sfxSound, x => x._name == name);
        if (S == null) { Debug.LogError("SFXSound not found"); }
        else
        {
            _sfxSource.PlayOneShot(S._audioClipsound);
            //Debug.Log("name" + name);
        }
    }

    public void MuteSound()
    {
        _musicSource.mute=!_musicSource.mute;
    }
    public void MuteSFX()
    {
        _sfxSource.mute=!_sfxSource.mute;   
    }
}
