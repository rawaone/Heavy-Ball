using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCube : MonoBehaviour
{
    public float speed = 1f;
    private Vector3 falling;
    bool isFallingDown;
    void Start()
    {
        isFallingDown = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFallingDown)
            FallingDown();
    }

    private void FallingDown()
    {
        isFallingDown = true;
        falling = new Vector3(0, -1, 0);
        this.transform.Translate(falling * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            StartCoroutine(FallingDownRoutine());
        }
        if(collision.transform.tag == "GameOver")
        {
            Destroy(gameObject);
        }
    }


    IEnumerator FallingDownRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        isFallingDown = true;
    }

}
