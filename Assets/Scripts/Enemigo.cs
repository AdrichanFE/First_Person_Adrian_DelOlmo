using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    private NavMeshAgent agent;
    private FirstPerson player;
    private Animator anim;
    private bool ventanaAtaque=false;
    [SerializeField] private float radioAtaque;
    [SerializeField] LayerMask queEsDanhable;
    [SerializeField] Transform atacckpoint;
    [SerializeField] private float dannoEnemigo;
    private bool dannoRealizado = false;


    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();

        player = GameObject.FindObjectOfType<FirstPerson>();
    }

    // Update is called once per frame
    void Update()
    {

        Perseguir();
        if (ventanaAtaque&& dannoRealizado==false)
        {
            DetectarJugador();
        }
        
    }

    private void DetectarJugador()
    {
         Collider[] collsDetectados= Physics.OverlapSphere(atacckpoint.position, radioAtaque, queEsDanhable);

        if(collsDetectados.Length > 0)
        {
            for(int i = 0;i<collsDetectados.Length;i++)
            {
                collsDetectados[i].GetComponent<FirstPerson>().RecibirDanno(dannoEnemigo);
            }
            dannoRealizado= true;
        }

    }

    private void Perseguir()
    {
        //Tengo que definir como destino la posicion del player.
        agent.SetDestination(player.transform.position);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;

            anim.SetBool("Attacking", true);

        }
    }

    //Evento de animacion
    private void FinAtaque()
    {
        agent.isStopped= false;
        dannoRealizado = false;
        anim.SetBool("Attacking", false);
        
    }

    private void AbrirVentanaAtaque()
    {
        ventanaAtaque= true;
    }
    private void CerrarVentanaAtaque()
    {
        ventanaAtaque = false;
    }
}
