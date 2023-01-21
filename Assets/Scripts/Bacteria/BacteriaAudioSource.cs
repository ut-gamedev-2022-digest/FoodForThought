using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaAudioSource : MonoBehaviour
{
    public GameObject AudioSourceObject;
    public static AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = AudioSourceObject.GetComponent<AudioSource>();
    }
}
