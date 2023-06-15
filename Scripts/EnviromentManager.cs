using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour
{
    [SerializeField] GameObject[] enviroments;
    [SerializeField] float zSpawn = 0;
    [SerializeField] float enviromentDistance = 40;
    [SerializeField] int numberOfEnviroments = 2;
    void Start()
    {
        SpawnEnviroment(0);
        SpawnEnviroment(1);
    }


    public void SpawnEnviroment(int enviromentIndex)
    {
        GameObject go = Instantiate(enviroments[enviromentIndex], transform.forward * zSpawn, Quaternion.identity);
        go.transform.parent = transform;
        zSpawn += enviromentDistance;
    }

}
