using UnityEngine;
using System.Collections;

public class MuerteJugador : MonoBehaviour
{
    private Animator animator;
    private PlayerMove scriptMovimiento; 
    private bool estaMuerto = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        scriptMovimiento = GetComponent<PlayerMove>();
    }

    public void Morir()
    {
        if (estaMuerto) return; 

        estaMuerto = true;
        Debug.Log(gameObject.name + " ha muerto.");

        if (scriptMovimiento != null) 
        {
            scriptMovimiento.enabled = false;
        }
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // CÓDIGO CORREGIDO PARA UNITY 6:
        // Buscamos el FinishManager con la nueva sintaxis para que sí encuentre el GameManager
        FinishManager managerFinal = Object.FindAnyObjectByType<FinishManager>();
        if (managerFinal != null)
        {
            managerFinal.JugadorMurio();
        }
        else
        {
            Debug.LogError("No se encontro el FinishManager en la escena.");
        }
    }

    public bool EstaMuerto() 
    { 
        return estaMuerto; 
    }
}