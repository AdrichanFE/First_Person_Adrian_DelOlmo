using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanManager : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    public void RegresarALaPartida()
    {
        Time.timeScale = 1.0f;
        menuPausa.SetActive(false);
    }

    public void RegresoMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }
    public void TerminarJuego()
    {
        Application.Quit();
    }
}
