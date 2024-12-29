using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCanvasManager : MonoBehaviour
{
    public void EmpezarPartida()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }
    public void TerminarJuego()
    {
        Application.Quit();
    }
}
