using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class ArmaAutomatica : MonoBehaviour
{
    [SerializeField] private ParticleSystem system;
    [SerializeField] private ArmaSO misDatos;
    private float timer;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        timer = misDatos.cadenciaAtaque;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButton(0)&& timer >= misDatos.cadenciaAtaque)
        {
            
                system.Play();
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, misDatos.distanciaAtaque))
                {
                    if (hit.transform.CompareTag("ParteEnemigo"))
                    {
                        hit.transform.GetComponent<ParteDeEnemigo>().RecibirDannoPlayer(misDatos.dannoAtaque);
                    }

                }
                timer = 0;
            
        }
    }
}
