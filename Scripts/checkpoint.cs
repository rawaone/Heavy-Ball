using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            GameManager.lastCheckPointPos = transform.position;
            //Debug.Log("checkpoint collied");
        }
    }

}
