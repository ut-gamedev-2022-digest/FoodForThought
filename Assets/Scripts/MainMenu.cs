using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject MainMenuPanel;

    public GameObject PausePanel;

    public TMP_InputField UsernameInputField;
    public GameObject RecordsPanel;
    public GameObject RecordsTable;
    public GameObject RecordsRowPrefab;
    public int ResultsNr = 5;

    private List<GameObject> recordsRows;

    private void Awake()
    {
        recordsRows = new List<GameObject>();
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }

        if (UsernameInputField != null)
        {
            var defaultUsername = ConstructDefaultUsername();
            UsernameInputField.text = defaultUsername;
        }

        if (RecordsPanel != null)
        {
            RecordsPanel.SetActive(false);
        }

        if (charactersPanel != null)
        {
            charactersPanel.SetActive(false);
        }

        if (MainMenuPanel != null)
        {
            MainMenuPanel.SetActive(true);
        }
    }

    private string ConstructDefaultUsername()
    {
        var usernamesNr = GetNumberOfUsernames();
        return "username_" + usernamesNr;
    }

    private int GetNumberOfUsernames()
    {
        return PlayerPrefs.GetInt(PlayerPrefsConstants.NumberOfUserNames, 0);
    }

    public void SaveUsername()
    {
        if (UsernameInputField == null) return;

        var username = UsernameInputField.text;
        var usernamesNr = GetNumberOfUsernames();
        PlayerPrefs.SetString(PlayerPrefsConstants.CurrentUserName, username);
        PlayerPrefs.SetInt(PlayerPrefsConstants.NumberOfUserNames, usernamesNr + 1);
        PlayerPrefs.Save();
    }

    public void PlayGame(bool silent = true)
    {
        if (!silent) audioSource.Play();

        var levelName = PlayerPrefs.GetString(PlayerPrefsConstants.SelectedLevelName);

        if (levelName == "") levelName = "Level1";

        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        audioSource.Play();
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1f;
        audioSource.Play();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        audioSource.Play();
        Events.ResumeGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        audioSource.Play();
        PausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadRecords()
    {
        audioSource.Play();
        if (MainMenuPanel != null && RecordsPanel != null && RecordsRowPrefab != null && RecordsTable != null)
        {
            MainMenuPanel.SetActive(false);
            RecordsPanel.SetActive(true);
            for (var i = 1; i <= ResultsNr; i++)
            {
                var username = PlayerPrefs.GetString("username_" + i, "-");
                var time = PlayerPrefs.GetString("time_" + i, "-");
                var row = Instantiate(RecordsRowPrefab, RecordsTable.transform);
                recordsRows.Add(row);
                var fields = row.GetComponentsInChildren<TextMeshProUGUI>();
                fields[0].text = i + ".";
                fields[1].text = username;
                fields[2].text = time;
            }
        }
    }

    public void BackFromRecords()
    {
        foreach (var row in recordsRows)
        {
            Destroy(row);
        }

        audioSource.Play();
        RecordsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    #region Characters

    public GameObject charactersPanel;

    public void LoadCharacters() => GoToMenu(MainMenuPanel, charactersPanel);

    public void BackFromCharacters() => BackToMainMenu(charactersPanel);


    public void SelectCharacter(int characterIndex)
    {
        audioSource.Play();
        PlayerPrefs.SetInt(PlayerPrefsConstants.SelectedCharacterIndex, characterIndex);
        PlayerPrefs.Save();
    }

    #endregion

    #region Levels

    public GameObject levelsPanel;
    
    public void LoadLevels() => GoToMenu(charactersPanel, levelsPanel);
    
    public void BackFromLevels() => BackToMainMenu(levelsPanel);

    public void SelectLevel(string levelName)
    {
        audioSource.Play();
        PlayerPrefs.SetString(PlayerPrefsConstants.SelectedLevelName, levelName);
        PlayerPrefs.Save();
    }

    #endregion

    private void BackToMainMenu(GameObject fromMenu)
    {
        audioSource.Play();
        MainMenuPanel.SetActive(true);
        fromMenu.SetActive(false);
    }

    private void GoToMenu(GameObject fromMenu, GameObject toMenu)
    {
        audioSource.Play();
        MainMenuPanel.SetActive(false);
        fromMenu.SetActive(false);
        toMenu.SetActive(true);
    }
}