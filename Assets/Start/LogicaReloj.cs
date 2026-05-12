using UnityEngine;
using TMPro;
using System.Collections;

public class ClockManager : MonoBehaviour
{
    [Header("Settings")]
    public TextMeshPro countdownText;
    public GameObject startLabel;
    public float remainingTime = 30f;

    private bool isTimerRunning = false;

    void Start()
    {
        if (startLabel != null)
            StartCoroutine(BounceEffect(startLabel.transform));

        Invoke("BeginGame", 1f);
    }

    void BeginGame()
    {
        if (startLabel != null)
            Destroy(startLabel);

        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
                remainingTime = 0;

            int seconds = Mathf.FloorToInt(remainingTime);
            countdownText.text = seconds.ToString("00");

            if (remainingTime < 6f)
                countdownText.color = Color.red;
        }
    }

    IEnumerator BounceEffect(Transform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 peakScale = originalScale * 1.5f;

        float timeTracker = 0;
        while (timeTracker < 1f)
        {
            timeTracker += Time.deltaTime * 8f;
            float sineWave = Mathf.Sin(timeTracker * Mathf.PI);
            target.localScale = Vector3.Lerp(originalScale, peakScale, sineWave);
            yield return null;
        }
        target.localScale = originalScale;
    }
}