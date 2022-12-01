using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum EndState
{
    LOST,
    WIN,
    WIN_WITH_RECORD
}
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
        ShowWinLosePanel(EndState.LOST, 0);
    }

    private void ReachFinish()
    {
        int place = CheckTimeForRecord();
        if (place == 0)
        {
            ShowWinLosePanel(EndState.WIN, 0);
        }
        else
        {
            ShowWinLosePanel(EndState.WIN_WITH_RECORD, place);
        }
        
    }

    private int CheckTimeForRecord()
    {
        float currentTime = Events.GetTime();
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        string prevTime = $"{minutes:00}:{seconds:00}";
        string prevUsername = PlayerPrefs.GetString("current_username", "default");
        bool found = false;
        int place = 0;
        for (int i = 1; i <= 5; i++)
        {
            string tmpTime = PlayerPrefs.GetString("time_" + i, "-");
            if (!found && (string.Compare(prevTime, tmpTime) < 0) || tmpTime.Equals("-"))
            {
                found = true;
                place = i;
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
        return place;
    }

    private void ShowTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        RemainingTimeMsg.text = $"Remaining time: {minutes:00}:{seconds:00}";
    }

    private void SetWinLoseMsg(EndState endState, int place)
    {
        WinLoseMsg.text = ConstructWinLoseMsg(endState, place);
    }

    private string ConstructWinLoseMsg(EndState endState, int place)
    {
        switch (endState)
        {
            case EndState.LOST: return "You lost!";
            case EndState.WIN: return "You won!";
            case EndState.WIN_WITH_RECORD: 
                switch (place)
                {
                    case 1: return "You won!\n1-st best time!";
                    case 2: return "You won!\n2-nd best time!";
                    case 3: return "You won!\n3-rd best time!";
                    case 4: return "You won!\n4-th best time!";
                    case 5: return "You won!\n5-th sbest time!";
                    default: return "You won!";
                }
                
            default: return "You lost!";
        }
        
    }

    private void ShowWinLosePanel(EndState endState, int place)
    {
        SetWinLoseMsg(endState, place);
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