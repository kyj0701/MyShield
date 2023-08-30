using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager I; 

    public GameObject square;
    public Text timeText;

    public GameObject endPanel;
    public Text thisScoreText;
    public Text bestScoreText;

    public Animator anim;

    float alive = 0f;
    bool isRunning = true;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("MakeSquare", 0.0f, 0.5f);
    }

    void Update()
    {
        if (isRunning)
        {
            alive += Time.deltaTime;
            timeText.text = alive.ToString("N2");
        }
    }

    void MakeSquare()
    {
        Instantiate(square);
    }

    public void GameOver()
    {
        anim.SetBool("isDie", true);

        isRunning = false;
        Invoke("TimeStop", 0.5f);
        thisScoreText.text = alive.ToString("N2");
        endPanel.SetActive(true);

        if (PlayerPrefs.HasKey("bestScore") == false)
        {
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore") < alive)
            {
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }

        bestScoreText.text = PlayerPrefs.GetFloat("bestScore").ToString("N2");
    }

    public void TimeStop()
    {
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
