using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    public static LevelSetup Instance;
    public Level Level;
    private void Awake()
    {
        Instance = this;
    }

    public List<GameObject> GetObstaclesFromBehind()
    {
        return Level.ObstacleFromBehindPrefabs;
    }
    

   
}
