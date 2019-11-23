using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public GameObject LevelDialog;
    public Text LevelStatus;
    public Text ScoreText;
    public static UIHandler instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void ShowLevelDialog(string status, string scores)
    {
        GetComponent<StarHandler>().starsAcheived();
        LevelDialog.SetActive(true);
        LevelStatus.text = status;
        ScoreText.text = scores;
    }
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
