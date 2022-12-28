using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaGenerator : MonoBehaviour
{
    public GameObject BacteriaPrefab;
    public int NrOfBacteria;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<NrOfBacteria; i++)
        {
            Vector3 position = transform.position + new Vector3(i*1f, i*1f, 0);
            Instantiate(BacteriaPrefab, position, Quaternion.identity);
        }
    }
}
