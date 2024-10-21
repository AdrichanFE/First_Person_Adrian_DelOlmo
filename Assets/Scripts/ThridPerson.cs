using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThridPerson : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float suavizado;
    private float velocidadRotacion;




    [Header("References")]




    CharacterController controller;
    private Camera cam;

    private float x, z;
    private float anguloRotacion;
    private Animator anim;

    Vector3 movimiento;
    Vector2 input;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }


    void Update()
    {
        MovimientoYRotacion();
    }

    private void MovimientoYRotacion()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, z).normalized;



        //Con sqrMagnitude es mas optimo que magnitude.
        if (input.sqrMagnitude > 0)
        {
            //Se calcula el angulo al que tengo que rotarme en funcion de los inputs y orientacion de camara.
            anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;//Con esto te tranforma de radianes a grados
            //Con esto suavizamos la rotacion del personaje.
            float anguloSuave = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloRotacion, ref velocidadRotacion, suavizado);

            transform.eulerAngles = new Vector3(0, anguloSuave, 0);//Con esto rota el personaje

            movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;//Para avanzar de frente hacia donde estas mirando, alineando mi frontal hcia donde apunte la camara.

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);//Con esto se mueve el personaje

            anim.SetBool("Walking", true);

        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }
}
