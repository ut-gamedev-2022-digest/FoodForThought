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
            animator.SetTrigger("Close");
            Events.EducationalWindowClose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        locationLabel.text = locationLabel.text == locationText ? backwardsLocationText : locationText;

        if (educated) return;

        educated = true;
        educationalLabel.text = educationalText;
        organ.sprite = organSprite;
        copyrightLabel.text = copyrightText;

        Events.EducationalWindowOpen();
    }

    private void OnEducationalWindowOpen()
    {
        animator.SetTrigger("Open");
        Time.timeScale = 0f;
        educational.SetActive(true);
    }

    private void OnEducationalWindowClose()
    {
        Time.timeScale = 1f;
        //educational.SetActive(false);
    }
}