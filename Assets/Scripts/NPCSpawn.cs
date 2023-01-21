using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public string Key;
    public GameObject StartTiggerWall;
    public GameObject EndTriggerWall;
    public bool SameType = true;
    private NPCSpawnData npcSpawnData;
    private float timeTillNextSpawn;
    private bool activated = false;
    private int prefabIndex = -1;

    private void SpawnObject(Vector3 position)
    {
        var prefab = GetPrefab();
        if (prefab != null)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }
    }

    private GameObject GetPrefab()
    {
        var prefabs = npcSpawnData.Prefabs;
        if (prefabIndex < 0)
        {
            if (prefabs.Count > 1)
            {
                prefabIndex = Random.Range(0, prefabs.Count);
            }
            else
            {
                prefabIndex = 0;
            }
        }
        else if (!SameType)
        {
            prefabIndex = Random.Range(0, prefabs.Count);
        }
        return prefabs[prefabIndex];
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
