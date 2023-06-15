using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    private Vector3 moving;
    public float speed = 0.1f;

     bool isMovigRight;
     bool isMovigLeft;

    void Start()
    {
        isMovigRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovigRight)
        MovingRight();

        if (isMovigLeft)
            MovingLeft();
    }

    private void MovingRight()
    {
        isMovigRight = true;
        moving = new Vector3(Random.Range(1,3), 0, 0);

        this.transform.Translate(moving * speed * Time.deltaTime);
    }

    private void MovingLeft()
    {
        isMovigLeft = true;
        moving = new Vector3(Random.Range(-1, -3), 0, 0);

        this.transform.Translate(moving * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "RightLine")
        {
            isMovigRight = false;
            isMovigLeft = true;
        }
        if (collision.transform.tag == "LeftLine")
        {
            isMovigRight = true;
            isMovigLeft = false;
        }
    }

    public void IncreaseCubeSpeedByLevel()
    {
        speed++;
    }

}
