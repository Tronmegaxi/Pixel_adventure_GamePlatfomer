using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPicker : MonoBehaviour
{
    #region SINGLETON
    private static CharacterPicker _instant;
    public static CharacterPicker Instant { get => _instant; }
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

    [SerializeField] Text _name;
    public PlayerImage[] _character;
    public int _characterIndex = 0;
    private void Update()
    {
        TheChosenOne();
        CharacterName();
    }
    public void NextButton()
    {
        MusicPlayer.Instant.PlaySFX("Button_Sound");
        if (_characterIndex == _character.Length-1 ) { _characterIndex = 0; }
        else
        {
            _characterIndex++;
        }
    }
    public void PreviousButton()
    {
        MusicPlayer.Instant.PlaySFX("Button_Sound");
        if (_characterIndex <= 0) { _characterIndex = _character.Length-1; }
        else
        {
            _characterIndex--;
        }
    }
    public void CharacterName()
    {
        _name.text = _character[_characterIndex]._name;
    }
    public void TheChosenOne()
    {
        for (int i = 0; i < _character.Length; i++)
        {
            _character[i]._player.gameObject.SetActive(false);
        }
        _character[_characterIndex]._player.gameObject.SetActive(true);
    }

}
