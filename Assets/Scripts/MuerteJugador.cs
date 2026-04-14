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

        
        if (scriptMovimiento != null) scriptMovimiento.enabled = false;
        
        
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        
        GameManager.instancia.RegistrarMuerte();
    }

    
    public bool EstaMuerto() 
    { 
        return estaMuerto; 
    }
}