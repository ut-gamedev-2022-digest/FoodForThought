using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Level Level;
    public ObstacleWaypoint FirstObstacleWaypoint;
    public static LoadLevel Instance;

    private void Awake()
    {
        Instance = this;   
    }
}
