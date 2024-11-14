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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CambioArma(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CambioArma(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CambioArma(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CambioArma(3);
        }
    }

    private void CambioArma(int nuevoIndice)
    { 
        //Desactivo el arma qeu actualmente llevo equipada
            armas[indiceArmaActual].SetActive(false);
        //Despues, cambio el indice
            indiceArmaActual = 0;
            armas[indiceArmaActual].SetActive(true);  
    }

}
