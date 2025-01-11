using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class FirstPerson : MonoBehaviour
{


    [Header("Stats")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float escalaGravedad;
    [SerializeField] private float radioDeteccion;
    [SerializeField] private float alturaSalto;
    [SerializeField] private float vidaActual;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private float lootActual;
    [SerializeField] private float lootMaximo;
    [SerializeField] private int recuperacion;
    [SerializeField] private int puntuacion;
    




    [Header("References")]
    [SerializeField] private Transform pies;
    [SerializeField] LayerMask queEsSuelo;
    [SerializeField] private Image barraVida;
    [SerializeField] private Image BarraLoot;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private GameObject menuVictoria;



    CharacterController controller;
    private Camera cam;

    private float x, z;
    private float anguloRotacion;

    Vector3 movimiento;
    Vector3 movimientoVertical;
    Vector2 input;


    private void FixedUpdate()//Ciclo de fisicas, es fijo. Se reproduce 0.02 segundos.
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            weaponHolder.SetActive(false);
            menuPausa.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Movimiento();
            
        }
        else
        {
            weaponHolder.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Movimiento();
        }
        
        

    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    
    void Update()
    {
        barraVida.fillAmount = vidaActual / vidaMaxima;
        BarraLoot.fillAmount = lootActual / lootMaximo;
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, z).normalized;

        DeteccionSuelo();
        AplicarGravedad();
       
    }

    //Es como un colisionEnter pero para un characterController
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.TryGetComponent(out ParteDeEnemigo cadaver))
        {
            Rigidbody rbEnemigo= hit.gameObject.GetComponent<Rigidbody>();
            Vector3 direccionFuerza=hit.transform.position-gameObject.transform.position;
            rbEnemigo.AddForce(direccionFuerza.normalized * 50, ForceMode.Impulse);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pocion"))
        {
            vidaActual += recuperacion;
            if (vidaActual >= vidaMaxima)
            {
                vidaActual = vidaMaxima;
            }
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Loot"))
        {
            lootActual += puntuacion;
            if (lootActual >= lootMaximo)
            {
                Time.timeScale = 0f;
                menuVictoria.SetActive(true);
                weaponHolder.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Movimiento();
            }
        }
        
    }

    void Movimiento()
    {
        //Con esto rotamos el cuerpo cuando rota la camara
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y, 0);

        //Con sqrMagnitude es mas optimo que magnitude.
        if (input.sqrMagnitude > 0)
        {
            //Se calcula el angulo al que tengo que rotarme en funcion de los inputs y orientacion de camara.
            anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;//Con esto te tranforma de radianes a grados
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);//Con esto rota el personaje

            movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;//Para avanzar de frente hacia donde estas mirando, alineando mi frontal hcia donde apunte la camara.

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);//Con esto se mueve el personaje

        }
    }

    void AplicarGravedad()
    {
        //Mi movimiento vertical en la y va aumentandose(+=) a cierta escala por segundo.
        movimientoVertical.y += escalaGravedad * Time.deltaTime;
        controller.Move(movimientoVertical*Time.deltaTime);
    }

    void DeteccionSuelo()
    {
        Collider[] collsDetectados= Physics.OverlapSphere(pies.position, radioDeteccion, queEsSuelo);
        if(collsDetectados.Length>0)
        {
            movimientoVertical.y = 0;
            Saltar();
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movimientoVertical.y = Mathf.Sqrt(-2 * escalaGravedad * alturaSalto);
        }
    }

    public void RecibirDanno(float dannoRecibido)
    {
        vidaActual -= dannoRecibido;
        if (vidaActual <= 0)
        {
            Time.timeScale = 0f;
            menuGameOver.SetActive(true);
            Destroy(gameObject);
        }
    }

   



    //Esto sirve para dibujar cualquier figura en la escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pies.position, radioDeteccion);
    }

}
