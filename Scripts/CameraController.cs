using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    Vector3 offset;
    [SerializeField] float smoothSpeed = 0.04f;

    void Start()
    {
        offset = transform.position - target.position;

        
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        //transform.position = newPosition;
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);

    }
}
