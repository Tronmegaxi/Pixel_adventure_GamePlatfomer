using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character : MonoBehaviour
{
    public PlayerImage[] _character;
    public int _characterIndex;
    void Start()
    {
        _characterIndex = (int)PlayerPrefsController.GetCharacter();
        TheChosenOne();
        
    }

    public void TheChosenOne()
    {
        for (int i = 0; i < _character.Length; i++)
        {
            _character[i]._player.gameObject.SetActive(false);
        }
        _character[_characterIndex]._player.gameObject.SetActive(true);
        FindObjectOfType<CamaraHolder>().Player = _character[_characterIndex]._player.gameObject.transform;
    }
}
