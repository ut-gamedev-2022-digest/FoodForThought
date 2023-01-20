using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Setups/Level setup")]
public class Level : ScriptableObject {

    public List<NPCSpawnData> NPCSpawnData;

    public NPCSpawnData GetNPCSpawnDataByName(string name)
    {
        foreach (var npcSpawnData in NPCSpawnData)
        {
            if (npcSpawnData.ObjectName.Equals(name))
            {
                return npcSpawnData;
            }
        }
        return null;
    }

}
