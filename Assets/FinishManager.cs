using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class FinishManager : MonoBehaviour
{
    [Header("Referencias de la UI")]
    public GameObject finishText;
    public GameObject finalMenu;
    public CanvasGroup fadePanel;
    public AudioSource audioSource; // <-- Ya está aquí integrado

    [Header("Configuración")]
    public float timeToFinish = 30f;
    public float delayAntesDelMenu = 1.5f;

    private bool finished = false;

    void Start()
    {
        if (finalMenu != null) finalMenu.SetActive(false);
        if (finishText != null) finishText.SetActive(false);
        if (fadePanel != null) fadePanel.alpha = 0f;
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

    public void JugadorMurio()
    {
        if (finished) return;
        finished = true;
        StartCoroutine(SecuenciaMuerte());
    }

    private IEnumerator SecuenciaMuerte()
    {
        // 1. Esperamos el tiempo de espera
        yield return new WaitForSecondsRealtime(delayAntesDelMenu);

        // 2. Reproducimos el sonido si está configurado
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // 3. Desvanecimiento a negro
        if (fadePanel != null)
        {
            while (fadePanel.alpha < 1f)
            {
                fadePanel.alpha += Time.unscaledDeltaTime * 2f;
                yield return null;
            }
        }

        // 4. Mostramos el menú final
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