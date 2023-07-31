using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    const string PLAYER_VOLUME_KEY = "Player Volume";
    const string PLAYER_SFX_KEY = "Player Volume";
    const string DIFFICULTY_KEY = "Difficulty";
    const string CHARACTER_KEY = "Character";
    const string STAR_LV1 = "Star Level 1";
    const string STAR_LV2 = "Star Level 2";
    const string STAR_LV3 = "Star Level 3";
    const string HIGH_SCORE_LV1 = "HighScore Level 1";
    const string HIGH_SCORE_LV2 = "HighScore Level 2";
    const string HIGH_SCORE_LV3 = "HighScore Level 3";

    //-------------------------------------------------------------
    public static void SetPlayerVolume(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_VOLUME_KEY, volume);
    }
    public static float GetPlayerVolume()
    {
        return PlayerPrefs.GetFloat(PLAYER_VOLUME_KEY);
    }
    //-------------------------------------------------------------
    public static void SetPlayerSFX(float volume)
    {
        PlayerPrefs.SetFloat(PLAYER_SFX_KEY, volume);
    }
    public static float GetPlayerSFX()
    {
        return PlayerPrefs.GetFloat(PLAYER_SFX_KEY);
    }
    //-------------------------------------------------------------
    public static void SetDifficulty(float volume)
    {
        PlayerPrefs.SetFloat(DIFFICULTY_KEY, volume);
    }
    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    }
    //-------------------------------------------------------------
    public static void SetCharacter(int volume)
    {
        PlayerPrefs.SetFloat(CHARACTER_KEY, volume);
    }
    public static float GetCharacter()
    {
        return PlayerPrefs.GetFloat(CHARACTER_KEY);
    }
    //-------------------------------------------------------------
    public static void SetStar_LV1(int volume)
    {
        PlayerPrefs.SetFloat(STAR_LV1, volume);
    }
    public static float GetStar_LV1()
    {
        return PlayerPrefs.GetFloat(STAR_LV1);
    }
    //-------------------------------------------------------------
    public static void SetStar_LV2(int volume)
    {
        PlayerPrefs.SetFloat(STAR_LV2, volume);
    }
    public static float GetStar_LV2()
    {
        return PlayerPrefs.GetFloat(STAR_LV2);
    }
    //-------------------------------------------------------------
    public static void SetStar_LV3(int volume)
    {
        PlayerPrefs.SetFloat(STAR_LV3, volume);
    }
    public static float GetStar_LV3()
    {
        return PlayerPrefs.GetFloat(STAR_LV3);
    }
    //-------------------------------------------------------------
    public static void SetHighScore_LV1(int volume)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_LV1, volume);
    }
    public static float GetHighScore_LV1()
    {
        return PlayerPrefs.GetFloat(HIGH_SCORE_LV1);
    }
    //-------------------------------------------------------------
    public static void SetHighScore_LV2(int volume)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_LV2, volume);
    }
    public static float GetHighScore_LV2()
    {
        return PlayerPrefs.GetFloat(HIGH_SCORE_LV2);
    }
    //-------------------------------------------------------------
    public static void SetHighScore_LV3(int volume)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_LV3, volume);
    }
    public static float GetHighScore_LV3()
    {
        return PlayerPrefs.GetFloat(HIGH_SCORE_LV3);
    }
}
