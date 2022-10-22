using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float TimeRemaining = 60;
    public bool IsRunning = false;
    public Text TimeText;

    private float _timeRemaining;

    private void Awake()
    {
        _timeRemaining = TimeRemaining;
        Events.OnTimeRunOut += TimeRunOut;
        Events.OnRestartGame += RestartTimer;
        Events.OnReachFinish += ReachFinish;
    }

    private void OnDestroy()
    {
        Events.OnTimeRunOut -= TimeRunOut;
        Events.OnRestartGame -= RestartTimer;
        Events.OnReachFinish -= ReachFinish;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsRunning = true;   
    }

    private void TimeRunOut()
    {
        TimeRemaining = 0;
        IsRunning = false;
        Events.ShowTime(0);
    }

    private void RestartTimer()
    {
        TimeRemaining = _timeRemaining;
        IsRunning = true;
    }

    private void ReachFinish()
    {
        IsRunning = false;
        Events.ShowTime(TimeRemaining + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            if (TimeRemaining > 0)
            {
                TimeRemaining -= Time.deltaTime;
                DisplayTime(TimeRemaining);
            }
            else
            {
                Events.TimeRunOut();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
