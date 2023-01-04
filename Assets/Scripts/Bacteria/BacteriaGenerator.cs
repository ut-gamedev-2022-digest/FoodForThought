using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaGenerator : MonoBehaviour
{
   
    public int NrOfBacteria;
    // Start is called before the first frame update

    void Start()
    {
        var bacteriaPrefabs = Game.Instance.Level.BacteriaPrebabs;
        int idx = Random.Range(0, bacteriaPrefabs.Count);
        if (bacteriaPrefabs.Count > 0)
        {
            for (int i = 0; i < NrOfBacteria; i++)
            {
                Vector3 position = transform.position + new Vector3(i * 1f, i * 1f, 0);
                Instantiate(bacteriaPrefabs[idx], position, Quaternion.identity);
            }
        }
    }
}
