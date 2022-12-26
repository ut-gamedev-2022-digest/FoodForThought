using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject CharactersPanel;

    private void Awake()
    {
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
        
        if (CharactersPanel != null)
        {
            CharactersPanel.SetActive(false);
        }
    }

    private string ConstructDefaultUsername()
    {
        var usernamesNr = GetNumberOfUsernames();
        return "username_" + usernamesNr;
    }

    private int GetNumberOfUsernames()
    {
        return PlayerPrefs.GetInt("usernames_nr", 0);
    }

    public void SaveUsername()
    {
        if (UsernameInputField == null) return;
        
        var username = UsernameInputField.text;
        var usernamesNr = GetNumberOfUsernames();
        PlayerPrefs.SetString("current_username", username);
        PlayerPrefs.SetInt("usernames_nr", usernamesNr + 1);
        PlayerPrefs.Save();
    }

    private void PlayGame(bool silent = true)
    {
        if (!silent) audioSource.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
                var fields = row.GetComponentsInChildren<TextMeshProUGUI>();
                fields[0].text = i + ".";
                fields[1].text = username;
                fields[2].text = time;
            }
        }
    }

    public void BackFromRecords()
    {
        audioSource.Play();
        RecordsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    #region Characters

    public void LoadCharacters()
    {
        audioSource.Play();
        if (MainMenuPanel == null || CharactersPanel == null) return;
        MainMenuPanel.SetActive(false);
        CharactersPanel.SetActive(true);
    }
    public void BackFromCharacters()
    {
        audioSource.Play();
        CharactersPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
    
    public void SelectCharacter(int characterIndex)
    {
        audioSource.Play();
        PlayerPrefs.SetInt("SelectedCharacterIndex", characterIndex);
        PlayerPrefs.Save();
        
        PlayGame();
    }

    #endregion

}