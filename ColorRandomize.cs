using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomize : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public Color[] colors = new Color[6];
    void Start()
    {
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {

       // meshRenderer.material.color = Random.ColorHSV();
      //  meshRenderer.material.color = colors[Random.Range(0, colors.Length)];
    }

    public void ChangeColorRoad(int colornum)
    {
        meshRenderer.material.color = colors[colornum];
    }
}
