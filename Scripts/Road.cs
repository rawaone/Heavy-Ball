using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    private Transform player;
    GameManager gameManager;
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;

        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.z > player.position.z)
       {
            //GameManager.numberofPassedRoads++;
      }
    }
}
