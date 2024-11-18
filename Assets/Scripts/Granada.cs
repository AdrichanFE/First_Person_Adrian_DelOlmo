using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float fuerzaImpulso;
    [SerializeField] private float tiempoVida;
    [SerializeField] private float radioExplosion;
    [SerializeField] private LayerMask queEsExplotable;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float fuerzaExplosion;
    [SerializeField] private float rebote;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward.normalized*fuerzaImpulso, ForceMode.Impulse);
        Destroy(gameObject, tiempoVida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Collider[] collsDetectados = Physics.OverlapSphere(transform.position, radioExplosion, queEsExplotable);
        if (collsDetectados.Length > 0)
        {
            foreach (Collider coll in collsDetectados)
            {
                coll.GetComponent<ParteDeEnemigo>().Explotar();
                coll.GetComponent<Rigidbody>().isKinematic = false;
                coll.GetComponent<Rigidbody>().AddExplosionForce(fuerzaExplosion, transform.position, radioExplosion, rebote,ForceMode.Impulse);
            }
            
        }
    }
}
