using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instancia;

    [Header("Configuración del Tiempo")]
    public float tiempoJuego = 60f;
    private bool juegoTerminado = false;

    [Header("Configuración de Thwomps")]
    public List<TrampaThwomp> enemigos; 
    public float intervaloAtaque = 2f;

    [Header("Configuración de Jugadores")]
    public List<MuerteJugador> jugadores; 

    void Awake()
    {
        
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        InvokeRepeating("LanzarAtaqueAleatorio", 1f, intervaloAtaque);
    }

    void Update()
    {
        if (juegoTerminado) return;

        
        if (tiempoJuego > 0)
        {
            tiempoJuego -= Time.deltaTime;
        }
        else
        {
            FinalizarJuego("¡Tiempo terminado! ¡Sobreviviste!");
        }
    }

    void LanzarAtaqueAleatorio()
    {
        if (juegoTerminado || enemigos.Count == 0) return;

        
        int indice = Random.Range(0, enemigos.Count);
        
        if (enemigos[indice] != null)
        {
            enemigos[indice].ActivarAtaque();
        }
    }

    
    public void RegistrarMuerte()
    {
        int sobrevivientes = 0;

        foreach (MuerteJugador jugador in jugadores)
        {
            if (jugador != null && !jugador.EstaMuerto())
            {
                sobrevivientes++;
            }
        }

        Debug.Log("Jugadores restantes: " + sobrevivientes);

        if (sobrevivientes <= 0)
        {
            FinalizarJuego("¡Todos han muerto! GAME OVER");
        }
    }

    void FinalizarJuego(string mensaje)
    {
        juegoTerminado = true;
        CancelInvoke("LanzarAtaqueAleatorio");
        Debug.Log(mensaje);

        
        Invoke("ReiniciarEscena", 3f);
    }

    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}