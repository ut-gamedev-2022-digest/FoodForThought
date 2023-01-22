using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float CooldownTime = 10f;
    public float AnimationSpeed = 1f;
    public bool Automatic;
    public TriggerWall TriggerWall;
    
    
    Animator animator;

    private float _openTime;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        Events.OnPauseGame += OnPauseGame;
        Events.OnResumeGame += OnResumeGame;
        Events.OnLost += OnLost;
        Events.OnEndGame += OnPauseGame;
    }

    private void OnDestroy()
    {
        Events.OnPauseGame -= OnPauseGame;
        Events.OnResumeGame -= OnResumeGame;
        Events.OnLost -= OnLost;
        Events.OnEndGame -= OnPauseGame;
    }

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.speed = AnimationSpeed;
        _openTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayAudioSource()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if ((animator != null) && Automatic && (Time.time > _openTime))
        {
            animator.SetTrigger("Open");
            PlayAudioSource();
            _openTime = Time.time + CooldownTime;
        }
        else if ((animator != null) && !Automatic && (TriggerWall != null) && TriggerWall.Activated)
        {
            animator.SetTrigger("Open");
            PlayAudioSource();
            TriggerWall.Activated = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(animator != null && collision.gameObject.CompareTag("Player"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("DoorsAnimation"))
            {
                var currentPosition = collision.gameObject.transform.position;
                collision.gameObject.transform.position = new Vector3(currentPosition.x + 5.0f, currentPosition.y, currentPosition.z);
            }
        }
    }

    private void OnLost(LoseReason _)
    {
        audioSource.Pause();
    }

    private void OnPauseGame()
    {
        audioSource.Pause();
    }

    private void OnResumeGame()
    {
        audioSource.UnPause();
    }


}
