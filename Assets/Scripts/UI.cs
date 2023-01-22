using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum EndState
{
    Lost,
    Win,
    WinWithRecord
}

public class UI : MonoBehaviour
{
    public GameObject WinLosePanel;
    public GameObject PausePanel;
    public GameObject EducationalPanel;
    public GameObject PlayerWrapper;
    public Text WinLoseMsg;
    public Text RemainingTimeMsg;
    public Animator animator;

    private AudioSource win;
    private AudioSource lose;

    private StarterAssetsInputs _input;

    private void Awake()
    {
        Events.OnLost += Lost;
        Events.OnRestartGame += HideWinLosePanel;
        Events.OnReachFinish += ReachFinish;
        Events.OnShowTime += ShowTime;
        Events.OnPauseGame += OnPauseGame;
        Events.OnResumeGame += OnResumeGame;
        Events.OnStartGame += OnStartGame;
        Events.OnEducationalWindowOpen += OnEducationalWindowOpen;
        Events.OnEducationalWindowClose += OnEducationalWindowClose;
        
        var audioSources = GetComponents<AudioSource>();
        win = audioSources[0];
        lose = audioSources[1];

        if (PlayerWrapper != null)
        {
            _input = PlayerWrapper.GetComponent<StarterAssetsInputs>();
        }
    }

    private void OnDestroy()
    {
        Events.OnLost -= Lost;
        Events.OnRestartGame -= HideWinLosePanel;
        Events.OnReachFinish -= ReachFinish;
        Events.OnShowTime -= ShowTime;
        Events.OnPauseGame -= OnPauseGame;
        Events.OnResumeGame -= OnResumeGame;
        Events.OnStartGame -= OnStartGame;
        Events.OnEducationalWindowOpen -= OnEducationalWindowOpen;
        Events.OnEducationalWindowClose -= OnEducationalWindowClose;
    }

    private void OnEducationalWindowClose()
    {
        if (EducationalPanel == null) return;
        EducationalPanel.SetActive(false);
        Time.timeScale = 1f;
        
        if (_input != null)
        {
            _input.cursorLocked = true;
            _input.cursorInputForLook = true;
        }
        
        Game.IsPaused = false;
    }

    private void OnEducationalWindowOpen()
    {
        if (EducationalPanel == null) return;
        Game.IsPaused = true;
        EducationalPanel.SetActive(true);
        Time.timeScale = 0f;
        
        if (_input != null)
        {
            _input.cursorLocked = false;
            _input.cursorInputForLook = false;
        }
    }

    private void OnStartGame()
    {
        Game.IsPaused = false;
    }

    private void OnResumeGame()
    {
        if (PausePanel == null) return;
        Game.IsPaused = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        
        if (_input != null)
        {
            _input.cursorLocked = true;
            _input.cursorInputForLook = true;
        }
    }

    private void OnPauseGame()
    {
        if (PausePanel == null) return;
        Game.IsPaused = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0f;
        
        if (_input != null)
        {
            _input.cursorLocked = false;
            _input.cursorInputForLook = false;
        }
    }

    private void Start()
    {
        WinLosePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Lost(LoseReason loseReason)
    {
        ShowWinLosePanel(EndState.Lost, 0);
    }

    private void ReachFinish()
    {
        var place = CheckTimeForRecord();
        if (place == 0)
        {
            ShowWinLosePanel(EndState.Win, 0);
        }
        else
        {
            ShowWinLosePanel(EndState.WinWithRecord, place);
        }
    }

    private static int CheckTimeForRecord()
    {
        var currentTime = Events.GetTime();
        var minutes = Mathf.FloorToInt(currentTime / 60);
        var seconds = Mathf.FloorToInt(currentTime % 60);
        var prevTime = $"{minutes:00}:{seconds:00}";
        var prevUsername = PlayerPrefs.GetString(PlayerPrefsConstants.CurrentUserName, "default");
        var found = false;
        var place = 0;
        for (var i = 1; i <= 5; i++)
        {
            var tmpTime = PlayerPrefs.GetString("time_" + i, "-");
            if (!found && (string.Compare(prevTime, tmpTime) < 0 || tmpTime.Equals("-")))
            {
                found = true;
                place = i;
            }

            if (!found) continue;
            var tmpUsername = PlayerPrefs.GetString("username_" + i, "username_" + i);
            PlayerPrefs.SetString("username_" + i, prevUsername);
            PlayerPrefs.SetString("time_" + i, prevTime);
            prevUsername = tmpUsername;
            prevTime = tmpTime;
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
        if (endState.Equals(EndState.Win) || endState.Equals(EndState.WinWithRecord))
        {
            win.Play();
        }
        else
        {
            lose.Play();
        }
    }

    private static string ConstructWinLoseMsg(EndState endState, int place)
    {
        return endState switch
        {
            EndState.Lost => "You lost!",
            EndState.Win => "You won!",
            EndState.WinWithRecord => place switch
            {
                1 => "You won!\n1-st best time!",
                2 => "You won!\n2-nd best time!",
                3 => "You won!\n3-rd best time!",
                4 => "You won!\n4-th best time!",
                5 => "You won!\n5-th best time!",
                _ => "You won!"
            },
            _ => "You lost!"
        };
    }

    private void ShowWinLosePanel(EndState endState, int place)
    {
        SetWinLoseMsg(endState, place);
        WinLosePanel.SetActive(true);

        animator.SetTrigger("Open");
        Time.timeScale = 0f;
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

    public void BackToMenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
    public void RestartGameButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ResumeGameButton()
    {
        Events.ResumeGame();
    }
    
    public void CloseEducationalPanelButton()
    {
        Events.EducationalWindowClose();
    }
}