using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Level Level;
    public ObstacleWaypoint FirstObstacleWaypoint;
    public GameObject Player;
    public GameObject BacteriaAudioSourceObject;
    public AudioSource BacteriaAudioSource;
    public static LoadLevel Instance;

    private void Awake()
    {
        Instance = this;
        BacteriaAudioSource = BacteriaAudioSourceObject.GetComponent<AudioSource>();
    }
}
