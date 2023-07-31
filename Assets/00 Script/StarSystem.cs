using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSystem : MonoBehaviour
{
    [SerializeField] int _Level;
    [SerializeField] SpriteRenderer[] _star;
    [SerializeField] Text _highScore;
    [SerializeField] int LV1, LV2, LV3;
    private void Start()
    {
        LV1 = (int)PlayerPrefsController.GetStar_LV1();
        LV2 = (int)PlayerPrefsController.GetStar_LV2();
        LV3 = (int)PlayerPrefsController.GetStar_LV3();
    }
    void Update()
    {
        GetLv();
    }
    void GetLv()
    {
        switch (_Level)
        {
            case 1:
                GetLV1();
                break;
            case 2:
                GetLV2();
                break;
            case 3:
                GetLV3();
                break;
            default:
                Debug.Log("out of LV");
                break;
        }
    }
    void GetLV1()
    {
        int star = (int)PlayerPrefsController.GetStar_LV1();
        for (int i = 0; i < _star.Length; i++)
        {
            _star[i].enabled = false;
        }
        _star[star].enabled = true;

        _highScore.text = "" + PlayerPrefsController.GetHighScore_LV1().ToString();
    }
    void GetLV2()
    {
        int star = (int)PlayerPrefsController.GetStar_LV2();
        for (int i = 0; i < _star.Length; i++)
        {
            _star[i].enabled = false;
        }
        _star[star].enabled = true;

        _highScore.text = "" + PlayerPrefsController.GetHighScore_LV2().ToString();
    }
    void GetLV3()
    {
        int star = (int)PlayerPrefsController.GetStar_LV3();
        for (int i = 0; i < _star.Length; i++)
        {
            _star[i].enabled = false;
        }
        _star[star].enabled = true;

        _highScore.text = "" + PlayerPrefsController.GetHighScore_LV3().ToString();
    }

}
