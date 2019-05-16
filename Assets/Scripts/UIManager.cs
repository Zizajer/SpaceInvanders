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
    public AudioClip EndGameAudioClip;
    public GameObject DisplayTextOnDeathObject;
    public float DisplayTextOnDeathObjectTime;
    private float score;
    private float PlayerLives;

    public float Score { get => score; set => score = value; }

    void Start()
    {
        Time.timeScale = 1;
        Score = 0;
        PlayerLives = 3;
    }
    
    void Update()
    {
        ScoreText.text = "Score :  " + Score;
        LivesText.text = "Lives :  " + PlayerLives;
    }

    public void IncreaseScore(float amount, Vector3 position, Color currentColor)
    {
        Score += amount;
        GameObject text = Instantiate(DisplayTextOnDeathObject,worldToUISpace(GetComponent<Canvas>(),position),new Quaternion(0,0,0,0),transform);
        text.GetComponent<ShortTimeDisplayText>().TextToDisplay = amount.ToString();
        text.GetComponent<ShortTimeDisplayText>().TimeCanBeDisplayed = DisplayTextOnDeathObjectTime;
        text.GetComponent<Text>().color = currentColor;

    }

    public void TakePlayerLife(float amount)
    {
        PlayerLives -= amount;
        if (PlayerLives <= 0)
        {
            SetEndGamePanel("LOST");
        }
    }


    public void SetEndGamePanel(string gameResult)
    {
        Time.timeScale = 0;
        GameObject endGamePanel = Instantiate(EndGamePanel, transform);
        endGamePanel.GetComponentInChildren<Text>().text = "YOU " + gameResult + " !!! Your Score  " + GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().score;
        endGamePanel.GetComponentInChildren<Button>().onClick.AddListener(() => RestartGame());
        Camera.main.GetComponent<AudioSource>().Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }

    private Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
}
