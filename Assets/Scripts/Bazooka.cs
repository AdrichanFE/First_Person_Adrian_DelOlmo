using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : MonoBehaviour
{
    [SerializeField] private GameObject granadePrefab;
    [SerializeField] private Transform granadeSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Creo una instancia con la misma orientacion que el cañon o un arco
            Instantiate(granadePrefab, granadeSpawn.position, granadeSpawn.rotation);


        }
    }
}
