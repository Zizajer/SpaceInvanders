using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text ScoreText;
    public Text LivesText;
    public GameObject EndGamePanel;
    private float Score;
    private float PlayerLives;
    
    void Start()
    {
        Time.timeScale = 1;
        Score = 0;
        PlayerLives = 3;
    }
    
    void Update()
    {
        ScoreText.text = "Score : " + Score;
        LivesText.text = "Lives : " + PlayerLives;

        if (PlayerLives <= 0)
        {
            Time.timeScale = 0;
            EndGamePanel.SetActive(true);
            EndGamePanel.GetComponentInChildren<Text>().text = "YOU LOST !!!";
        }
    }

    public void IncreaseScore(float amount)
    {
        Score += amount;
    }

    public void TakePlayerLife(float amount)
    {
        PlayerLives -= amount;
    }

    public void SetGameWon()
    {
        Time.timeScale = 0;
        EndGamePanel.SetActive(true);
        EndGamePanel.GetComponentInChildren<Text>().text = "YOU WIN !!!";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }
}
