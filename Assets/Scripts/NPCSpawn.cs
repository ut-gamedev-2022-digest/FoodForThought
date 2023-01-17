using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public string Key;
    public GameObject StartTiggerWall;
    public GameObject EndTriggerWall;
    private NPCSpawnData npcSpawnData;
    private float timeTillNextSpawn;
    private bool activated = false;

    private void SpawnObject(Vector3 position)
    {
        var prefabs = npcSpawnData.Prefabs;
        GameObject prefab = null;
        if(prefabs.Count == 1)
        {
            prefab = prefabs[0];
        }
        else if (prefabs.Count > 1)
        {
            int idx = Random.Range(0, prefabs.Count);
            prefab = prefabs[idx];
        }
        if (prefab != null)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
    }

    private void SpawnObjects()
    {
        int minObejctsNrInSpawn = npcSpawnData.MinObjectsNrInSpawn;
        int maxObjectsNrInSpawn = npcSpawnData.MaxObjectsNrInSpawn;
        int nrOfObjects = minObejctsNrInSpawn;
        if (minObejctsNrInSpawn != maxObjectsNrInSpawn)
        {
            nrOfObjects = Random.Range(minObejctsNrInSpawn, maxObjectsNrInSpawn + 1);
        }
        
        for (int i = 0; i < nrOfObjects; i++)
        {
            Vector3 position = transform.position + new Vector3(i * 1f, i * 1f, 0);
            SpawnObject(position);
        }
        timeTillNextSpawn = Time.time + npcSpawnData.DurationBetweenSpawns;
    }
    void Start()
    {
        npcSpawnData = LoadLevel.Instance.Level.GetNPCSpawnDataByName(Key);
        if (StartTiggerWall == null)
        {
            activated = true;
        }
        if (npcSpawnData != null && activated)
        {
            SpawnObjects();
        }
    }

    void CheckActivation(GameObject TriggerWall)
    {
        if(TriggerWall != null) {
            var triggerWall = TriggerWall.GetComponent<TriggerWall>();
            if (triggerWall != null && triggerWall.Activated)
            {
                activated = !activated;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            CheckActivation(EndTriggerWall);

        }
        else
        {
            CheckActivation(StartTiggerWall);
        }
        if(npcSpawnData != null && activated && !npcSpawnData.OneTimeSpawn && Time.time > timeTillNextSpawn)
        {
            SpawnObjects();
        }
    }
}
