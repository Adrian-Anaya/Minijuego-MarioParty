using UnityEngine;

public class FinishManager : MonoBehaviour
{
    public GameObject finishText;
    public AudioSource finishSound;

    float time = 30f;
    bool finished = false;

    void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0 && !finished)
        {
            finishText.SetActive(true);
            finishSound.Play();

            StartCoroutine(BounceEffect());

            finished = true;
        }
    }

    System.Collections.IEnumerator BounceEffect()
    {
        Transform t = finishText.transform;

        t.localScale = Vector3.zero;

        while (t.localScale.x < 1.2f)
        {
            t.localScale += Vector3.one * Time.deltaTime * 3;
            yield return null;
        }

        while (t.localScale.x > 1f)
        {
            t.localScale -= Vector3.one * Time.deltaTime * 3;
            yield return null;
        }

        t.localScale = Vector3.one;
    }
}                                                                                                                                                                 