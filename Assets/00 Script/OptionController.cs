using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;
    [SerializeField] Slider _difficultySlider;
    [SerializeField] float _defaultVolume = 0.5f;
    [SerializeField] int _defaultDifficulty = 1;
    LevelLoadController LevelLoadController;
    void Start()
    {
        _volumeSlider.value = PlayerPrefsController.GetPlayerVolume();
        _difficultySlider.value = PlayerPrefsController.GetDifficulty();
        LevelLoadController = FindObjectOfType<LevelLoadController>();
    }
    void Update()
    {
        MusicPlayer.Instant.SetSoundVolume(_volumeSlider.value);
    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetPlayerVolume(_volumeSlider.value);
        PlayerPrefsController.SetDifficulty(_difficultySlider.value);
        PlayerPrefsController.SetCharacter(CharacterPicker.Instant._characterIndex);
        LevelLoadController.LoadMainMenuScreen();
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    public void DefaultSeting()
    {
        _volumeSlider.value = _defaultVolume;
        _difficultySlider.value = _defaultDifficulty;
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
}
