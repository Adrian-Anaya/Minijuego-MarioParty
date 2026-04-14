using UnityEngine;
using System.Collections;

public class TrampaThwomp : MonoBehaviour
{
    public float bajarVelocidad = 10f;
    public float subirVelocidad = 2f;
    public float tiempoEsperaAbajo = 1f;
    
    private Vector3 posicionOriginal;
    private bool estaAtacando = false;

    void Start() {
        posicionOriginal = transform.position;
    }

    public void ActivarAtaque() {
        if (!estaAtacando) StartCoroutine(SecuenciaAtaque());
    }

    IEnumerator SecuenciaAtaque() {
        estaAtacando = true;

        
        Vector3 posicionSuelo = new Vector3(transform.position.x, -2.30f, transform.position.z); 
        while (Vector3.Distance(transform.position, posicionSuelo) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, posicionSuelo, bajarVelocidad * Time.deltaTime);
            yield return null;
        }

        
        yield return new WaitForSeconds(tiempoEsperaAbajo);

        
        while (Vector3.Distance(transform.position, posicionOriginal) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, posicionOriginal, subirVelocidad * Time.deltaTime);
            yield return null;
        }

        estaAtacando = false;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            
            MuerteJugador scriptMuerte = collision.gameObject.GetComponent<MuerteJugador>();
            
            if (scriptMuerte != null)
            {
                scriptMuerte.Morir();
            }
        }
    }
}