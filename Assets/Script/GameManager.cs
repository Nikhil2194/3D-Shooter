using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timeRemaining = 360f;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public GameObject timeTextobject;
    int score;

    private void Awake()
    {
        timeTextobject.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= 1 * Time.deltaTime;
            float sec = Mathf.FloorToInt(timeRemaining % 360);
            timeText.text = "Time Left: " + sec.ToString() + " seconds";
        }
    }

    public void PlayGameScene()
    {
        SceneManager.LoadScene(1);
        timeTextobject.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        timeTextobject.SetActive(false);
        AudioManager.Instance.MusicSource.Stop();
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
