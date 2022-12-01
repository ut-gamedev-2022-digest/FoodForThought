using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject WinLosePanel;
    public Text WinLoseMsg;
    public Text RemainingTimeMsg;
    
    private void Awake()
    {
        Events.OnLost += Lost;
        Events.OnRestartGame += HideWinLosePanel;
        Events.OnReachFinish += ReachFinish;
        Events.OnShowTime += ShowTime;
    }

    private void OnDestroy()
    {
        Events.OnLost -= Lost;
        Events.OnRestartGame -= HideWinLosePanel;
        Events.OnReachFinish -= ReachFinish;
        Events.OnShowTime -= ShowTime;
    }

    private void Start()
    {
        WinLosePanel.SetActive(false);
    }

    private void Lost(LoseReason loseReason)
    {
        ShowWinLosePanel(false);
    }

    private void ReachFinish()
    {
        CheckTimeForRecord();
        ShowWinLosePanel(true);
    }

    private void CheckTimeForRecord()
    {
        float currentTime = Events.GetTime();
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        string prevTime = $"{minutes:00}:{seconds:00}";
        string prevUsername = PlayerPrefs.GetString("current_username", "default");
        bool found = false;
        for (int i = 1; i <= 5; i++)
        {
            string tmpTime = PlayerPrefs.GetString("time_" + i, "-");
            if (!found && (string.Compare(prevTime, tmpTime) < 0) || tmpTime.Equals("-"))
            {
                found = true;
            }
            if (found)
            {
                string tmpUsername = PlayerPrefs.GetString("username_" + i, "username_" + i);
                PlayerPrefs.SetString("username_" + i, prevUsername);
                PlayerPrefs.SetString("time_" + i, prevTime);
                prevUsername = tmpUsername;
                prevTime = tmpTime;
            }
        }
        PlayerPrefs.Save();
    }

    private void ShowTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        RemainingTimeMsg.text = $"Remaining time: {minutes:00}:{seconds:00}";
    }

    private void SetWinLoseMsg(bool win)
    {
        WinLoseMsg.text = win ? "You won!" : "You lost!";
    }

    private void ShowWinLosePanel(bool win)
    {
        SetWinLoseMsg(win);
        WinLosePanel.SetActive(true);
    }

    private void HideWinLosePanel()
    {
        WinLosePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartClicked()
    {
        Events.RestartGame();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}