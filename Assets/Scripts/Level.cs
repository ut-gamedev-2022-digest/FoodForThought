using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setups/Level setup")]
public class Level : ScriptableObject {
    public List<GameObject> BacteriaPrebabs;
    public List<GameObject> ObstacleFromBehindPrefabs;
    public List<GameObject> WormPrefabs;
    public GameObject DoorPrefab;
    
}
