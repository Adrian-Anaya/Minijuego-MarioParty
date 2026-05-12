using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement; // This is necessary to change scenes

public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene"); // game scene
    }

    public void Salir()
    {
        Application.Quit();
        UnityEngine.Debug.Log("Saliendo...");
    }
}

