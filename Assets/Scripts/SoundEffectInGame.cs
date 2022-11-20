using UnityEngine;

public class SoundEffectInGame : MonoBehaviour
{
    public AudioSource audioSourcePrefab;
    public bool loop = true;
    public AudioClip clip;
    [Range(0, 1)] public float volume = 1;

    private AudioSource _audioSource;

    private void Awake()
    {
        Events.OnEndGame += Stop;
        Events.OnPauseGame += PauseMenuVolume;
        Events.OnResumeGame += ResumeMenuVolume;
    }
    
    private void OnDestroy()
    {
        Events.OnEndGame -= Stop;
        Events.OnPauseGame -= PauseMenuVolume;
        Events.OnResumeGame -= ResumeMenuVolume;
    }

    private void Start()
    {
        if (clip == null) return;
        
        _audioSource = Instantiate(audioSourcePrefab);
        _audioSource.clip = clip;
        _audioSource.loop = loop;
        _audioSource.volume = volume;
        _audioSource.Play();
    }

    private void Play()
    {
        if (_audioSource == null) return;
        _audioSource.Play();
    }
    
    private void Stop()
    {
        if (_audioSource == null) return;
        _audioSource.Stop();
    }
    
    private void PauseMenuVolume()
    {
        if (_audioSource == null) return;
        _audioSource.volume = volume * 0.3f;
    }
    
    private void ResumeMenuVolume()
    {
        if (_audioSource == null) return;
        _audioSource.volume = volume;
    }
}