using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshPro countdownText;      // Drag your "30" text here
    public GameObject startLabel;        // Drag your "START" text here
    public float remainingTime = 30f;    // Initial time

    private bool isTimerRunning = false;

    void Start()
    {
        // 1. Start the visual bounce effect on the START label
        if (startLabel != null)
        {
            StartCoroutine(BounceEffect(startLabel.transform));
        }

        // 2. Schedule the StartLabel to disappear and the timer to begin in 1 seconds
        Invoke("BeginGame", 1f);
    }

    // Logic to hide the label and trigger the clock
    void BeginGame()
    {
        if (startLabel != null)
        {
            Destroy(startLabel);
        }
        isTimerRunning = true;
    }

    void Update()
    {
        // Only decrease time if the game has started and there is time left
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Clamp time so it never goes below 00
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            // Display the time as an integer with two digits (e.g., "09")
            int seconds = Mathf.FloorToInt(remainingTime);
            countdownText.text = seconds.ToString("00");

            // Change color to red when reaching the last 5 seconds
            if (remainingTime < 6f)
            {
                countdownText.color = Color.red;
            }
        }
    }

    // Visual effect to make the text pop/bounce
    IEnumerator BounceEffect(Transform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 peakScale = originalScale * 1.5f; // Grow by 30%

        float timeTracker = 0;
        while (timeTracker < 1f)
        {
            timeTracker += Time.deltaTime * 8f; // Animation speed
            float sineWave = Mathf.Sin(timeTracker * Mathf.PI);
            target.localScale = Vector3.Lerp(originalScale, peakScale, sineWave);
            yield return null;
        }
        target.localScale = originalScale;
    }
}









