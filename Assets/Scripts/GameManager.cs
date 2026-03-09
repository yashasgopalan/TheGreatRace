using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("Input Acions Data")]
    [SerializeField] InputAction inputactions;
    
    [Header("Scene Change Data")]
    [SerializeField] string sceneName = "MazeScene";
    [SerializeField] bool gameStarted = false;

    [Header("Gameplay Canvas Data")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI itemsCollected;

    [Header("Button Canvas Data")]
    [SerializeField] Canvas startCanvas;
    [SerializeField] Button startButton;
    [SerializeField] Canvas retryCanvas;
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

        startCanvas.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        retryCanvas.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

        //TODO: Disable Player MOVEMENT
    }

    public void StartGame()
    {
        gameStarted = true;

        //TODO: Enable Player MOVEMENT
        startButton.gameObject.SetActive(false);
        startCanvas.gameObject.SetActive(false);
    }

    public void Retry()
    {
        SceneManager.LoadScene(sceneName);
    }

    void SetRetryUI()
    {
        startCanvas.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        retryCanvas.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
    }
}
