using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Necesario para las corrutinas

public class FinishManager : MonoBehaviour
{
    [Header("Referencias de la UI")]
    public GameObject finishText;
    public GameObject finalMenu;
    public CanvasGroup fadePanel; // Panel negro con un componente CanvasGroup

    [Header("Configuración")]
    public float timeToFinish = 30f;
    public float delayAntesDelMenu = 1.5f; // Tiempo para ver la animación de muerte

    private bool finished = false;

    void Start()
    {
        if (finalMenu != null) finalMenu.SetActive(false);
        if (finishText != null) finishText.SetActive(false);
        if (fadePanel != null) fadePanel.alpha = 0f; // Pantalla transparente al inicio
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (!finished)
        {
            timeToFinish -= Time.deltaTime;
            if (timeToFinish <= 0) TerminarPorVictoria();
        }
    }

    public void TerminarPorVictoria()
    {
        if (finished) return;
        finished = true;
        ActualizarUI("¡FINISH!");
        if (finalMenu != null) finalMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    // Se llama desde el script MuerteJugador
    public void JugadorMurio()
    {
        if (finished) return;
        finished = true;
        // Iniciamos la secuencia de transición diferida
        StartCoroutine(SecuenciaMuerte());
    }

    private IEnumerator SecuenciaMuerte()
    {
        // 1. Esperamos un momento para ver la animación de muerte de la rana
        yield return new WaitForSecondsRealtime(delayAntesDelMenu);

        // 2. Desvanecimiento a negro (Fade Out) si el panel existe
        if (fadePanel != null)
        {
            while (fadePanel.alpha < 1f)
            {
                fadePanel.alpha += Time.unscaledDeltaTime * 2f; // Velocidad del fade
                yield return null;
            }
        }

        // 3. Mostramos el menú y pausamos el juego
        ActualizarUI("¡GAME OVER!");
        if (finalMenu != null) finalMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void ActualizarUI(string mensaje)
    {
        if (finishText != null) 
        {
            finishText.SetActive(true);
            var tmp = finishText.GetComponent<TextMeshProUGUI>();
            if (tmp != null) tmp.text = mensaje;
        }
    }

    public void Reiniciar() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir() => Application.Quit();
}                                                                                              