using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishManager : MonoBehaviour
{
    public GameObject finishText;
    public AudioSource finishSound;
    public GameObject finalMenu;
    public float timeToFinish = 30f;

    private bool finished = false;

    void Start()
    {
        if (finalMenu != null) finalMenu.SetActive(false);
        if (finishText != null) finishText.SetActive(false);
    }

    void Update()
    {
        if (!finished)
        {
            timeToFinish -= Time.deltaTime;
            if (timeToFinish <= 0)
            {
                TerminarJuego();
            }
        }
    }

    public void TerminarJuego()
    {
        if (finished) return;
        finished = true;

        if (finishText != null) finishText.SetActive(true);
        if (finishSound != null) finishSound.Play();
        if (finalMenu != null) finalMenu.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego
    }

    // ESTA ES LA FUNCIÓN QUE FALTABA Y CAUSABA EL ERROR ROJO
    public void JugadorMurio()
    {
        if (!finished)
        {
            if (finishText != null) 
            {
                var tmp = finishText.GetComponent<TextMeshProUGUI>();
                if (tmp != null) tmp.text = "¡GAME OVER!";
            }
            TerminarJuego();
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}                                                                                                               