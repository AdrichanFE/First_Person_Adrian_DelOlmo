using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{


    [Header("Stats")]
    [SerializeField] private float velocidadMovimiento;
    



    [Header("References")]



    CharacterController controller;

    private float x, z;
    private float anguloRotacion;

    Vector3 movimiento;
    Vector2 input;

    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        input = new Vector2(x, z).normalized;

        if (input.magnitude>0)
        {
            //Se calcula el angulo al que tengo que rotarme en funcion de los inputs y orientacion de camara.
            anguloRotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg+Camera.main.transform.eulerAngles.y;//Con esto te tranforma de radianes a grados
            transform.eulerAngles = new Vector3(0, anguloRotacion, 0);//Con esto rota el personaje

            movimiento = Quaternion.Euler(0, anguloRotacion, 0) * Vector3.forward;

            controller.Move(movimiento * velocidadMovimiento * Time.deltaTime);

        }    
    }
  
}
