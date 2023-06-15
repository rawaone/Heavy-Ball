using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private Vector3 rotation;
    public float speed = 1f;
    void Start()
    {
            rotation = new Vector3(0, Random.Range(15,20), 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }

    public void IncreaseRotateSpeedByLevel()
    {
        speed++;
    }

}
