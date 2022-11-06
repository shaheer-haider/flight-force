using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenuPanel;
    public GameObject mainMenuPanel;
    public List<GameObject> graphicsToggleButtons = new List<GameObject>();

    private void Start()
    {
        toggleGraphicsButtons(QualitySettings.GetQualityLevel());
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
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
