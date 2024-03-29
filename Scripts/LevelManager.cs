﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("UI references :")]
    [SerializeField] private Image uiFillImage;

    [Header("Player & Endline references :")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform endLineTransform;
    //[SerializeField] GameObject endlineprefab;

    // "endLinePosition" to cache endLine's position to avoid
    // "endLineTransform.position" each time since the End line has a fixed position.
    private Vector3 endLinePosition;

    // "fullDistance" stores the default distance between the player & end line.
    private float fullDistance;




    private void Start()
    {
       // endLinePosition = endlineprefab.transform.position;
        endLinePosition = endLineTransform.position;
        fullDistance = GetDistance();
    }


    private float GetDistance()
    {
        // Slow
        return Vector3.Distance (playerTransform.position, endLinePosition) ;

        // Fast
        //return (endLinePosition - playerTransform.position).sqrMagnitude;
    }


    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }


    private void Update()
    {
        // check if the player doesn't pass the End Line
        if (playerTransform.position.z <= endLinePosition.z)
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }
    }


}
