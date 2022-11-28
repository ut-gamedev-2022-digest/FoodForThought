using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Location : MonoBehaviour
{
    public TextMeshProUGUI locationLabel;
    public string locationText;
    public string backwardsLocationText;
    public TextMeshProUGUI educationalLabel;
    public TextMeshProUGUI copyrightLabel;
    public string copyrightText;
    public string educationalText;
    public GameObject educational;
    public bool educated;
    public Image organ;
    public Sprite organSprite;

    private void Start()
    {
        educational.SetActive(false);
        educated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Banana")
        {
            if (locationLabel.text == locationText)
            {
                locationLabel.text = backwardsLocationText;
            }
            else locationLabel.text = locationText;
            if (!educated)
            {
                educated = true;
                Time.timeScale = 0f;
                educationalLabel.text = educationalText;
                organ.sprite = organSprite;
                copyrightLabel.text = copyrightText;
                educational.SetActive(true);
            }
        }
    }
}
