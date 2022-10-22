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
        Events.OnTimeRunOut += TimeRunOut;
        Events.OnRestartGame += HideWinLosePanel;
        Events.OnReachFinish += ReachFinish;
        Events.OnShowTime += ShowTime;
    }

    private void OnDestroy()
    {
        Events.OnTimeRunOut -= TimeRunOut;
        Events.OnRestartGame -= HideWinLosePanel;
        Events.OnReachFinish -= ReachFinish;
        Events.OnShowTime -= ShowTime;
    }


    void Start()
    {
        WinLosePanel.SetActive(false);
    }

    private void TimeRunOut()
    {
        ShowWinLosePanel(false);
    }

    private void ReachFinish()
    {
        ShowWinLosePanel(true);
    }

    private void ShowTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        RemainingTimeMsg.text = string.Format("Remaining time: {0:00}:{1:00}", minutes, seconds);
    }

    private void SetWinLoseMsg(bool win)
    {
        if (win)
        {
            WinLoseMsg.text = "You won!";
        }
        else
        {
            WinLoseMsg.text = "You lost!";
        }
    }

    public void ShowWinLosePanel(bool win)
    {
        SetWinLoseMsg(win);
        WinLosePanel.SetActive(true);
    }

    public void HideWinLosePanel()
    {
        WinLosePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartClicked()
    {
        Events.RestartGame();
    }
}
