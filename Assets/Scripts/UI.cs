using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject WinLosePanel;
    public Text WinLoseMsg;

    private void Awake()
    {
        Events.OnTimeRunOut += TimeRunOut;
        Events.OnRestartGame += HideWinLosePanel;
    }

    private void OnDestroy()
    {
        Events.OnTimeRunOut -= TimeRunOut;
        Events.OnRestartGame -= HideWinLosePanel;
    }


    void Start()
    {
        WinLosePanel.SetActive(false);
    }

    private void TimeRunOut()
    {
        ShowWinLosePanel(false);
    }

    private void SetWinLoseMsg(Text winLoseMsg, bool win)
    {
        if (win)
        {
            winLoseMsg.text = "You won!";
        }
        else
        {
            winLoseMsg.text = "You lost!";
        }
    }

    public void ShowWinLosePanel(bool win)
    {
       
        SetWinLoseMsg(WinLoseMsg, win);
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
