using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Location : MonoBehaviour
{
    public TextMeshProUGUI locationLabel;
    public string locationText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Banana")
        {
            locationLabel.text = locationText;
        }
    }
}
