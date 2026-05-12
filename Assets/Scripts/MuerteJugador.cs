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

        // Busca el script de arriba y lo activa
        FinishManager managerFinal = FindObjectOfType<FinishManager>();
        if (managerFinal != null)
        {
            managerFinal.JugadorMurio();
        }
    }

    public bool EstaMuerto() 
    { 
        return estaMuerto; 
    }
}