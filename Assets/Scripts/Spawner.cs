using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private Transform[] puntosSpawn;
    [SerializeField] private Enemigo enemigoPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemigos());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemigos()
    {
        while (1 == 1)
        {
            Instantiate(enemigoPrefab, puntosSpawn[0].position, Quaternion.identity);
            yield return new WaitForSeconds(4);
            Instantiate(enemigoPrefab, puntosSpawn[1].position, Quaternion.identity);
            yield return new WaitForSeconds(4);
            Instantiate(enemigoPrefab, puntosSpawn[2].position, Quaternion.identity);

        }
    }
}
