using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParteDeEnemigo : MonoBehaviour
{
    [SerializeField] private Enemigo mainScript;
    [SerializeField] private float multiplicadorDanno;
    public void RecibirDannoPlayer(float dannoRecibido)
    {
        
        mainScript.Vida -= dannoRecibido*multiplicadorDanno;
        if (mainScript.Vida <= 0)
        {
            mainScript.Morir();
        }
    }
}
