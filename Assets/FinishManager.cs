using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishManager : MonoBehaviour
{
    public GameObject finishText;
    public AudioSource finishSound;
    public GameObject finalMenu;
    public float timeToFinish = 30f; // Tiempo inicial

    private bool finished = false;

    void Start()
    {
        // Al empezar, nos aseguramos de que el menú esté oculto
        if (finalMenu != null) finalMenu.SetActive(false);
        if (finishText != null) finishText.SetActive(false);
        
        // Resetear el tiempo de escala por si acaso
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!finished)
        {
            // El tiempo baja cada segundo
            timeToFinish -= Time.deltaTime;

            // Si quieres ver el tiempo en la consola, quita las barras de la línea de abajo:
            // Debug.Log("Tiempo restante: " + timeToFinish);

            if (timeToFinish <= 0)
            {
                TerminarPorVictoria();
            }
        }
    }

    // Se llama cuando el tiempo llega a 0
    public void TerminarPorVictoria()
    {
        if (finished) return;
        finished = true;

        if (finishText != null) 
        {
            finishText.SetActive(true);
            var tmp = finishText.GetComponent<TextMeshProUGUI>();
            if (tmp != null) tmp.text = "¡FINISH!"; // Texto de victoria
        }

        MostrarMenuFinal();
    }

    // Se llama desde el script MuerteJugador
    public void JugadorMurio()
    {
        if (finished) return;
        finished = true;

        if (finishText != null) 
        {
            finishText.SetActive(true);
            var tmp = finishText.GetComponent<TextMeshProUGUI>();
            if (tmp != null) tmp.text = "¡GAME OVER!"; // Texto de derrota
        }

        MostrarMenuFinal();
    }

    private void MostrarMenuFinal()
    {
        if (finishSound != null) finishSound.Play();
        if (finalMenu != null) finalMenu.SetActive(true);

        Time.timeScale = 0f; // Pausa el juego
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