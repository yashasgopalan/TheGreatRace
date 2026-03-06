using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderGraph.Internal;

public class GameManager : MonoBehaviour
{

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
        countDown = timer;
    }

    void Update()
    {
        ConvertToMMSS();
    }

    void ConvertToMMSS()
    {
        countDown = Mathf.Max(0, countDown - Time.deltaTime);
        int minutes = Mathf.FloorToInt(countDown / 60);
        int seconds = Mathf.FloorToInt(countDown % 60);

        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
