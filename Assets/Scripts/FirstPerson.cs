using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{


    [Header("Stats")]
    [SerializeField] private float velocidadMovimiento;
    



    [Header("References")]




    CharacterController controller;
    private Camera cam;

    private float x, z;
    private float anguloRotacion;

    Vector3 movimiento;
    Vector2 input;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
    }

    
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, z).normalized;

        //Con sqrMagnitude es mas optimo que magnitude.
        if (input.sqrMagnitude>0)
        {
            //Se calcula el angulo al que tengo que rotarme en funcion de los inputs y orientacion de camara.
            anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg+cam.transform.eulerAngles.y;//Con esto te tranforma de radianes a grados
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);//Con esto rota el personaje

            movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;//Para avanzar de frente hacia donde estas mirando, alineando mi frontal hcia donde apunte la camara.

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);//Con esto se mueve el personaje

        }    
    }
  
}
