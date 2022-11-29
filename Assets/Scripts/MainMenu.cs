using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public AudioSource audioSource;
    public TMP_InputField UsernameInputField;
    public GameObject MainMenuPanel;
    public GameObject RecordsPanel;
    public GameObject RecordsTable;
    public GameObject RecordsRowPrefab;
    public int ResultsNr = 5;

    private void Awake()
    {
        if (UsernameInputField != null)
        {
            string defaultUsername = ConstructDefaultUsername();
            UsernameInputField.text = defaultUsername;
        }
        if (RecordsPanel != null)
        {
            RecordsPanel.SetActive(false);
        }
    }

    private string ConstructDefaultUsername()
    {
        int usernamesNr = GetNumberOfUsernames();
        return "username_" + usernamesNr;
    }

    private int GetNumberOfUsernames()
    {
        return PlayerPrefs.GetInt("usernames_nr", 0);
    }

    private void SaveUsername()
    {
        if(UsernameInputField != null)
        {
            string username = UsernameInputField.text;
            int usernamesNr = GetNumberOfUsernames();
            PlayerPrefs.SetString("current_username", username);
            PlayerPrefs.SetInt("usernames_nr", usernamesNr + 1);
            PlayerPrefs.Save();
        }
        
    }
    public void PlayGame()
    {
        SaveUsername();
        audioSource.Play();
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
            for (int i = 1; i <= ResultsNr; i++)
            {
                string username = PlayerPrefs.GetString("username_" + i, "-");
                string time = PlayerPrefs.GetString("time_" + i, "-");
                var row = Instantiate(RecordsRowPrefab, RecordsTable.transform);
                var fields = row.GetComponentsInChildren<TextMeshProUGUI>();
                fields[0].text = i.ToString() + ".";
                fields[1].text = username.ToString();
                fields[2].text = time.ToString();
            }
        }
    }

    public void BackFromRecords()
    {
        audioSource.Play();
        RecordsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        
    }
}
