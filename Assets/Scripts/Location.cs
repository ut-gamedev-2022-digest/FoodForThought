using TMPro;
using UnityEngine;
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
    public Animator animator;

    private void Awake()
    {
        Events.OnEducationalWindowOpen += OnEducationalWindowOpen;
        Events.OnEducationalWindowClose += OnEducationalWindowClose;
    }

    private void OnDestroy()
    {
        Events.OnEducationalWindowOpen -= OnEducationalWindowOpen;
        Events.OnEducationalWindowClose -= OnEducationalWindowClose;
    }

    private void Start()
    {
        educational.SetActive(false);
        educated = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Events.EducationalWindowClose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Events.EducationalWindowOpen();
    }

    private void OnEducationalWindowOpen()
    {
        if (animator != null) animator.SetTrigger("Open");
        
        locationLabel.text = locationText;

        if (educated) return;

        educated = true;
        educationalLabel.text = educationalText;
        organ.sprite = organSprite;
        copyrightLabel.text = copyrightText;
    }

    private void OnEducationalWindowClose()
    {
        if (animator != null) animator.SetTrigger("Close");
    }
}