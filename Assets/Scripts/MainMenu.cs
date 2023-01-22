using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject MainMenuPanel;

    public TMP_InputField UsernameInputField;
    public GameObject RecordsPanel;
    public GameObject RecordsTable;
    public GameObject RecordsRowPrefab;
    public int ResultsNr = 6;

    private List<GameObject> recordsRows;

    private void Awake()
    {
        recordsRows = new List<GameObject>();

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
        Game.ResetLevelsUnlockedForCurrentUser();
        Application.Quit();
    }

    public void LoadRecords()
    {
        audioSource.Play();
        if (MainMenuPanel != null && RecordsPanel != null && RecordsRowPrefab != null && RecordsTable != null)
        {
            MainMenuPanel.SetActive(false);
            RecordsPanel.SetActive(true);
            var level = "1 lvl";
            for (var i = 1; i <= ResultsNr; i++)
            {
                var username = PlayerPrefs.GetString("username_" + i, "-");
                var time = PlayerPrefs.GetString("time_" + i, "-");
                var row = Instantiate(RecordsRowPrefab, RecordsTable.transform);
                recordsRows.Add(row);
                var fields = row.GetComponentsInChildren<TextMeshProUGUI>();
                var nr = i;
                if (i > 3)
                {
                    level = "2 lvl";
                    nr = i - 3;
                }
                fields[0].text = nr + ".";
                fields[1].text = level;
                fields[2].text = username;
                fields[3].text = time;
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