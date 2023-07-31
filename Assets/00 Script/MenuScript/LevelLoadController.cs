using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoadController : MonoBehaviour
{
    int currentSceneIndex;
    [SerializeField] int SplashScreenTime = 3;
    [SerializeField] GameObject _winPanel;
    int OptionScreen = 5, MainMenuScreen = 1;
    private void Start()
    {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(Wait4Time());
            LoadScene("01 MenuScreen");
        }
        else
        {
            return;
        }

        if (_winPanel == null)
        {
            return;
        }
        else { _winPanel.gameObject.SetActive(false); }

    }
    private void FixedUpdate()
    {
        WinCodition();
    }
    IEnumerator Wait4Time()
    {
        yield return new WaitForSeconds(SplashScreenTime);
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    #region LoadOptionScreen
    public void LoadOptionScreen()
    {
        StartCoroutine(WaitOptionScreen());
        LoadScene("02 OptionScreen");
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    IEnumerator WaitOptionScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }
    #endregion
    #region LoadMainMenuScreen
    public void LoadMainMenuScreen()
    {
        StartCoroutine(WaitMainMenuScreen());
        LoadScene("01 MenuScreen");
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    IEnumerator WaitMainMenuScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(MainMenuScreen);
    }
    #endregion
    #region LoadLevel1
    public void LoadLoadLevel_1_Screen()
    {
        StartCoroutine(WaitLoadLevel_1_Screen());
        LoadScene("Level 1 Instruction");
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    IEnumerator WaitLoadLevel_1_Screen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }
    #endregion
    #region LoadLevel2
    public void LoadLoadLevel_2_Screen()
    {
        StartCoroutine(WaitLoadLevel_2_Screen());
        LoadScene("Level 2");
        MusicPlayer.Instant.PlaySFX("Button_Sound");

    }
    IEnumerator WaitLoadLevel_2_Screen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }
    #endregion
    #region LoadLevel3
    public void LoadLoadLevel_3_Screen()
    {
        StartCoroutine(WaitLoadLevel_3_Screen());
        LoadScene("Level 3");
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    IEnumerator WaitLoadLevel_3_Screen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }
    #endregion
    #region ReloadScene
    public void ReloadScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    #endregion

    #region Quit
    public void Quit()
    {
        StartCoroutine(WaitQuitScreen());
        MusicPlayer.Instant.PlaySFX("Button_Sound");
        Application.Quit();
    }
    IEnumerator WaitQuitScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }



    #endregion
    #region LoadLevelScreen
    public void LoadLevelScreen()
    {
        StartCoroutine(WaitLoadLevelScreen());
        LoadScene("03 Level Screen");
        MusicPlayer.Instant.PlaySFX("Button_Sound");
    }
    IEnumerator WaitLoadLevelScreen()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(OptionScreen);
    }
    #endregion
    #region Transition Screen
    [SerializeField] public TransitionSettings transition;
    [SerializeField] public float startDelay;
    public void LoadScene(string _sceneName)
    {
        TransitionManager.Instance().Transition(_sceneName, transition, startDelay);
    }
    #endregion
    #region WinPanel
    public void WinCodition()
    {
        if (!FindObjectOfType<Flag>() || !_winPanel)
        {
            MusicPlayer.Instant._musicSource.UnPause();
            return;
        }
        else if (!FindObjectOfType<Flag>().IsWin)
        {
            _winPanel.gameObject.SetActive(false);
        }
        else
        {
            _winPanel.gameObject.SetActive(true);
            MusicPlayer.Instant._musicSource.Pause();
        }


    }
    #endregion
    #region ResetScore
    public void ResetScore()
    {
        PlayerPrefsController.SetHighScore_LV1(0);
        PlayerPrefsController.SetHighScore_LV2(0);
        PlayerPrefsController.SetHighScore_LV3(0);
        PlayerPrefsController.SetStar_LV1(0);
        PlayerPrefsController.SetStar_LV2(0);
        PlayerPrefsController.SetStar_LV3(0);
        PlayerPrefsController.SetCharacter(0);
    }
    #endregion



}
