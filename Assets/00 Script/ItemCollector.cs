using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;
public class ItemCollector : MonoBehaviour
{
    [SerializeField] private int _Fuits = 0;
    [SerializeField] private int _Coin = 0;

    [SerializeField] private Text FruitsText;
    PlayerHealth _getheart;
    GameObject _gameObj;
    private void Start()
    {
        _getheart = this.GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        CheckHighScore();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits"))
        {
            _Fuits++;
            FruitsText.text = "" + _Fuits;
            MusicPlayer.Instant.PlaySFX("GetFruit");
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            _Coin++;
            MusicPlayer.Instant.PlaySFX("GetCoin");
        }
        else if (collision.gameObject.CompareTag("Heart"))
        {
            _getheart.Health++;
            MusicPlayer.Instant.PlaySFX("GetHeart");
        }
        else
        {
            return;
        }
        _gameObj = collision.gameObject;
        collision.gameObject.GetComponent<Animator>().SetBool("isCollect", true);
        Invoke("Deactive", 0.1f);
    }
    void CheckHighScore()
    {
        int highScore = (int)PlayerPrefsController.GetHighScore_LV1();
        int coin = (int)PlayerPrefsController.GetStar_LV1();
        if (_Fuits > highScore)
        {
            PlayerPrefsController.SetHighScore_LV1(_Fuits);
        }
        if (_Coin > coin)
        {
            PlayerPrefsController.SetStar_LV1(_Coin);
        }
    }
    void Deactive()
    {
        _gameObj.gameObject.SetActive(false);
    }
}
