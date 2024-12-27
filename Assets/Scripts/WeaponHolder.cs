using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private GameObject[] armas;

    int indiceArmaActual=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CambiarArmaConTeclado();
        CambiarArmaConRaton();
    }


    private void CambiarArmaConRaton()
    {
        //Lectura de la rueda del raton(subir y bajar)
        float scrollWheel=Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel > 0)
        {
            CambioArma(indiceArmaActual - 1);
        }
        else if (scrollWheel < 0)
        {
            CambioArma(indiceArmaActual + 1);
        }
    }
    private void CambiarArmaConTeclado()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CambioArma(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CambioArma(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            CambioArma(2);
        }
        //if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        //{
        //    CambioArma(3);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        //{
        //    CambioArma(4);
        //}
    }

    private void CambioArma(int nuevoIndice)
    { 
        

        //Solo si es un indice valido puedo intercambiar el arma
        if (nuevoIndice>=0 && nuevoIndice < armas.Length)
        {
            //Desactivo el arma qeu actualmente llevo equipada
            armas[indiceArmaActual].SetActive(false);
            //Despues, cambio el indice
            indiceArmaActual = nuevoIndice;
            armas[indiceArmaActual].SetActive(true);
        }

          
    }

}
