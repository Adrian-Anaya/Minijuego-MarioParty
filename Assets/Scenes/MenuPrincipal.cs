using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager: MonoBehaviour
{
    [Header("Transition Configuration")]
    public Animator transitionAnimator; // Drag your "Fundido" Image here
    public float sceneLoadDelay = 1f;    // Time in seconds to wait for the animation

    void Start()
    {
        // Force the animator to stay asleep at launch so it doesn't trigger the dark frame
        if (transitionAnimator != null)
        {
            transitionAnimator.enabled = false;
        }
    }

    // This is the main function linked to your Start Button
    public void Jugar()
    {
        StartCoroutine(ExecuteSceneChange());
    }

    private IEnumerator ExecuteSceneChange()
    {
        if (transitionAnimator != null)
        {
            // 1. Wake up the animator component right when the button is clicked
            transitionAnimator.enabled = true;

            // 2. Play the fade transition state
            transitionAnimator.Play("FadeOut");
        }

        // 3. Wait in the background for the delay duration
        yield return new WaitForSeconds(sceneLoadDelay);

        // 4. Perform the clean jump to the gameplay arena
        SceneManager.LoadScene("SampleScene");
    }

    public void TerminateGame()
    {
        Application.Quit();
        Debug.Log("Application session closed.");
    }
}





