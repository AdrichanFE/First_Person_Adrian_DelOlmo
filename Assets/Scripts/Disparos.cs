using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour
{

    [SerializeField] private ParticleSystem system;
    [SerializeField] private ArmaSO misDatos;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //cam es la camera principal de la escena "MainCamera"
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
            system.Play();
            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, misDatos.distanciaAtaque))
            {
                hit.transform.GetComponent<Enemigo>().RecibirDannoPlayer(misDatos.dannoAtaque);
            }
        }
    }
}
