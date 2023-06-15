using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButtonImage : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Sprite newImageSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImageButton()
    {
        button.image.sprite = newImageSprite;
    }
}
