using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenuPanel;
    public GameObject mainMenuPanel;
    public List<GameObject> graphicsToggleButtons = new List<GameObject>();
    public GameObject loadingScreen;
    public Slider loadingSlider;

    bool isLoadingGame = false;
    AsyncOperation loadingOperation;

    public BannerAd bannerAd;

    private void Start()
    {
        isLoadingGame = false;
        toggleGraphicsButtons(QualitySettings.GetQualityLevel());
    }
    public void PlayGame()
    {
        bannerAd.ShowBannerAd();
        if (!isLoadingGame)
        {
            isLoadingGame = true;
            StartCoroutine(LoadSceneAsync("Game"));
        }
    }

    // load scene async and show loading bar in UI using coroutine
    IEnumerator LoadSceneAsync(string sceneName)
    {
        mainMenuPanel.SetActive(false);
        loadingScreen.SetActive(true);
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadingOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void openMainMenu()
    {
        optionsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void openOptionsMenu()
    {
        optionsMenuPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void toggleGraphicsButtons(int index)
    {
        for (int i = 0; i < graphicsToggleButtons.Count; i++)
        {
            if (i == index)
            {
                graphicsToggleButtons[i].SetActive(true);
            }
            else
            {
                graphicsToggleButtons[i].SetActive(false);
            }
            setGraphicsQuality(i);
        }
    }
    public void setGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}
