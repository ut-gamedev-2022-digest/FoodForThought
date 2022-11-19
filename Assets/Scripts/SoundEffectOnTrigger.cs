using UnityEngine;

public class SoundEffectOnTrigger : MonoBehaviour
{
    public AudioSource audioSourcePrefab;
    public AudioClip clipOnEnter;
    public AudioClip clipOnExit;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = Instantiate(audioSourcePrefab);
    }
    
    private void PlaySound(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (clipOnEnter == null) return;
        
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            PlaySound(clipOnEnter);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (clipOnExit == null) return;
        
        var keyboardControl = other.GetComponent<PlayerKeyboardControl>();
        if (keyboardControl != null)
        {
            PlaySound(clipOnExit);
        }
    }
}