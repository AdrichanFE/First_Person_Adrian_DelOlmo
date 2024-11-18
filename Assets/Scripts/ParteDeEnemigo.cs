using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

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

    public void Explotar()
    {
        mainScript.GetComponent<Animator>().enabled = false;
        mainScript.GetComponent<NavMeshAgent>().enabled = false;
        mainScript.enabled = false;
    }
}
