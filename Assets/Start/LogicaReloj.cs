using UnityEngine;
using TMPro;
using System.Collections; // Necesario para el rebote

public class LogicaReloj : MonoBehaviour
{
    public TextMeshPro textoReloj;
    public GameObject objetoADesaparecer;
    public float tiempoRestante = 30f;
    private bool marcha = false;

    void Start()
    {
        // 1. Iniciamos el efecto de rebote
        if (objetoADesaparecer != null)
        {
            StartCoroutine(EfectoRebote(objetoADesaparecer.transform));
        }

        // 2. Programamos la desaparición en 1 segundo
        Invoke("DesaparecerYEmpezar", 1f);
    }

    IEnumerator EfectoRebote(Transform objetivo)
    {
        Vector3 escalaOriginal = objetivo.localScale;
        Vector3 escalaGrande = escalaOriginal * 1.5f; // Crece un 20%

        float tiempo = 0;
        while (tiempo < 1f)
        {
            tiempo += Time.deltaTime * 8f; // Velocidad del rebote
            // Crea una onda de rebote matemática (Seno)
            float curvatura = Mathf.Sin(tiempo * Mathf.PI);
            objetivo.localScale = Vector3.Lerp(escalaOriginal, escalaGrande, curvatura);
            yield return null;
        }
        objetivo.localScale = escalaOriginal;
    }

    void DesaparecerYEmpezar()
    {
        if (objetoADesaparecer != null) Destroy(objetoADesaparecer);
        marcha = true;
    }

    void Update()
    {
        if (marcha && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante < 0) tiempoRestante = 0;
            int tiempoAMostrar = Mathf.FloorToInt(tiempoRestante);
            textoReloj.text = tiempoAMostrar.ToString("00");
            if (tiempoRestante < 6f) textoReloj.color = Color.red;
        }
    }
}








