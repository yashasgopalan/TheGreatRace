using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Scene Change Data")]
    [SerializeField] string sceneName = "MazeScene";
    [SerializeField] bool gameStarted = false;

    [Header("Gameplay Canvas Data")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI itemsCollected;

    [Header("Button Canvas Data")]
    [SerializeField] Canvas buttonCanvas;
    [SerializeField] Button startButton;
    [SerializeField] Button retryButton;

    [TooltipAttribute("A value of 1 is equal to 1s. If you need 10s you input 10.")]
    [SerializeField] private float timer = 180.0f;
    private float countDown;

    void Start()
    {
        gameStarted = false;
        countDown = timer;

        SetStartUI();
    }

    void Update()
    {
        ConvertToMMSS();
    }

    void ConvertToMMSS()
    {
        if(!gameStarted)
        {
            return;
        }

        if(countDown <= 0)
        {   
            //TODO: Enabled Retry UI(MUST HAVE)
            SetRetryUI();

            //TODO: Stop PlayerMovement and enable Retry UI(NICE TO HAVE)
            return;
        }

        countDown = Mathf.Max(0, countDown - Time.deltaTime);
        int minutes = Mathf.FloorToInt(countDown / 60);
        int seconds = Mathf.FloorToInt(countDown % 60);

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void SetStartUI()
    {
        gameStarted = false;

        buttonCanvas.gameObject.SetActive(true);

        retryButton.gameObject.SetActive(false);

        startButton.gameObject.SetActive(true);

        //TODO: Disable Player MOVEMENT
    }

    void StartGame()
    {
        gameStarted = true;

        //TODO: Enable Player MOVEMENT
        startButton.gameObject.SetActive(false);
        buttonCanvas.gameObject.SetActive(false);
    }

    void Retry()
    {
        SceneManager.LoadScene(sceneName);
    }

    void SetRetryUI()
    {
        buttonCanvas.gameObject.SetActive(true);

        startButton.gameObject.SetActive(false);

        retryButton.gameObject.SetActive(true);
    }
}
