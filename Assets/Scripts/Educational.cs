using UnityEngine;

public class Educational : MonoBehaviour
{
    public AudioSource audioSource;

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        audioSource.Play();
    }
}