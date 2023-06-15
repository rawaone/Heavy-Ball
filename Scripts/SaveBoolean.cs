using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveBoolean : MonoBehaviour
{

    [System.Serializable]
    public class Items
    {
    public bool isPurchased;
    public int isActive = 0;
    }

    public List<Items> items;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(isActive == 1)
        //{
        //    isPurchased = true;
        //}
    }

    public void ActiveAndSave()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[2].isActive = 1;
        }
        SavePurchase();
    }

    public void SavePurchase()
    {
        //    PlayerPrefs.SetInt("purchased", isActive);
        //    

        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetInt("purchased", items[2].isActive);
        }
        
        Debug.Log("saved");
    }

    public void LoadPurchase()
    {
        //    isActive = PlayerPrefs.GetInt("purchased");
        //    Debug.Log("Loaded");

        for (int i = 0; i < items.Count; i++)
        {
            items[2].isActive = PlayerPrefs.GetInt("purchased");
        }
    }

}
