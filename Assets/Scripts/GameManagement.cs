using System.Collections;
using System.Collections.Generic;
using HeneGames.Airplane;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public static int playerScore = 0;
    public GameManagement instance;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public LevelManagement levelManagement;
    public SimpleAirPlaneController airPlaneController;
    int isRevived;
    public GameObject loadingScreen;

    public InsertialAd insertialAd;


    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
        {
            instance = this;
        }
        insertialAd = FindObjectOfType<InsertialAd>();
        levelManagement = FindObjectOfType<LevelManagement>();

    }
    void Start()
    {
        playerScore = 0;
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        airPlaneController = FindObjectOfType<SimpleAirPlaneController>();
        isRevived = PlayerPrefs.GetInt("revive", 0);
        if (isRevived == 1)
        {
            PlayerPrefs.SetInt("revive", 0);
            playerScore = PlayerPrefs.GetInt("old_score", 0);
            PlayerPrefs.SetInt("old_score", 0);
            isRevived = 0;
            gameOverPanel.SetActive(false);
        }
        scoreText.text = playerScore.ToString();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
        if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", playerScore);
            highScoreText.text = playerScore.ToString();
        }
    }

    public void pauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void openMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void gameOver()
    {
        insertialAd.LoadAd();
        gameOverPanel.SetActive(true);
    }
    public static void revive()
    {
        PlayerPrefs.SetInt("revive", 1);
        PlayerPrefs.SetInt("old_score", playerScore);
        SceneManager.LoadScene("Game");
    }
}
