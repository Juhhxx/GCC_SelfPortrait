using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timertext;
    [SerializeField] private float time;
    [SerializeField] private bool isInitializer;
    [SerializeField] private GameObject otherTimer;
    private bool timerRun = true;
    private float timerStore;
    private WinCheck win;
    private GameObject gameOverScreen;
    private GameObject cameraManager;
    void Start()
    {
        win = FindFirstObjectByType<WinCheck>();
        gameOverScreen = FindFirstObjectByType<Lost>().gameObject;
        cameraManager = FindFirstObjectByType<WebCamPhoto>().gameObject;
        timerStore = time;
    }
    void Update()
    {
        if (timerRun)
        {
            time -= Time.deltaTime;

            int seconds = (int) (time % 60) % 60;
            int minutes = (int) time / 60;

            if (isInitializer)
                timertext.text = string.Format("{0:0}",seconds);
            else
                timertext.text = string.Format("{0:0}:{1:00}",minutes,seconds);

            if (time < 0)
                StopTimer();
        }
        else
        {
            if (isInitializer)
            {
                cameraManager.GetComponent<WebCamPhoto>().PhotoTaker();
                otherTimer.GetComponent<Timer>().RestartTimer();
                gameObject.SetActive(false);
            }
            else
            {
                if (!win.HasWon)
                {
                    Debug.Log("Time has run out!");
                    gameOverScreen.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
    }
    public void StopTimer()
    {
        timerRun = false;
    }
    public void RestartTimer()
    {
        time = timerStore;
        timerRun = true;
    }
}
