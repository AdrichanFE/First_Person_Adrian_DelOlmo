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
    [SerializeField] private GameObject[] items;
    [SerializeField] private float radioAtaque;
    [SerializeField] LayerMask queEsDanhable;
    [SerializeField] Transform atacckpoint;
    [SerializeField] private float dannoEnemigo;
    [SerializeField] private float vida;
    private bool dannoRealizado = false;
    private Rigidbody[] huesos;

    public float Vida { get => vida; set => vida = value; }


    // Start is called before the first frame update
    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        anim=GetComponent<Animator>();
        huesos=GetComponentsInChildren<Rigidbody>();
        player = GameObject.FindObjectOfType<FirstPerson>();

        CambiarEstadoHuesos(true);

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
    public void Morir()
    {
        agent.enabled = false;
        anim.enabled = false;
        CambiarEstadoHuesos(false);
        GameObject randomItem = items[Random.Range(0, items.Length)];
        Instantiate(randomItem, this.gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject, 4);
    }

    private void CambiarEstadoHuesos(bool estado)
    {
        for (int i = 0; i < huesos.Length; i++)
        {
            huesos[i].isKinematic = estado;
        }
    }

    private void Perseguir()
    {
        //Tengo que definir como destino la posicion del player.
        agent.SetDestination(player.transform.position);
        //Si no hay calculos pendientes para saber donde esta mi objetivo
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;

            anim.SetBool("Attacking", true);

            EnfocarPlayer();

        }
    }

    private void EnfocarPlayer()
    {
        Vector3 direccionAPlayer= (player.transform.position-transform.position).normalized;
        direccionAPlayer.y = 0;
        transform.rotation = Quaternion.LookRotation(direccionAPlayer);
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
