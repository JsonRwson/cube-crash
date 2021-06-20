using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{
    public GameObject LoadScene;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject HUD;
    public GameObject BlurVolume;

    private CanvasGroup GameOverCanvas;

    void Start()
    {
        BlurVolume.SetActive(false);
        PauseMenu.SetActive(false);
        GameOverMenu.SetActive(false);
        HUD.SetActive(true);
        GameOverCanvas = GameOverMenu.GetComponent<CanvasGroup>();
        Application.targetFrameRate = 144;
    }

    public void Pause()
    {
        HUD.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        HUD.SetActive(false);
        GameOverMenu.SetActive(true);
        BlurVolume.SetActive(true);
        GameOverCanvas.alpha = 1;
    }

    public void ResumeGame()
    {
        HUD.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}