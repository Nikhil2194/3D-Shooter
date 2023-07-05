using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyScript : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private GameObject GameLogo;
    [SerializeField] private GameObject GameWallpaper;

    [SerializeField] private GameObject InstructionBG;
    [SerializeField] private GameObject GameStartButton;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);

        LeanTween.scale(GameWallpaper,new Vector3 (1, 1, 1f), 0.9f).setDelay(0.4f);
        LeanTween.move(GameLogo.GetComponent<RectTransform>(), new Vector3(0f, -44f, 0f), 1.2f).setDelay(0.9f);
        LeanTween.move(Buttons.GetComponent<RectTransform>(), new Vector3(0f,-323.04f, 0f), 1.9f).setDelay(2.9f);

    }

    public void InstructionScreen()
    {
        GameLogo.SetActive(false);
        Buttons.SetActive(false);
        InstructionBG.SetActive(true);
        GameStartButton.SetActive(true);
    }

    private void PlayGame()
    {
        InstructionScreen();
    }

    private void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
