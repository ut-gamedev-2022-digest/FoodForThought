using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setups/NPCSpawn setup")]
public class NPCSpawnData : ScriptableObject
{
    public string ObjectName;
    public List<GameObject> Prefabs;
    public int MinObjectsNrInSpawn = 1;
    public int MaxObjectsNrInSpawn = 1;
    public bool OneTimeSpawn = true;
    public float DurationBetweenSpawns = 0f;
}
