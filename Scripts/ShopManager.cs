using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [System.Serializable]
    public class UnlockableMatrixx
    {
        public bool hasHealthPerk;
        public bool hasCoinsPerk;
        public bool hasStaminaPerk;
    }

    public UnlockableMatrixx unlockableMatrix;

    public Button healthButton, coinButton, staminButton;

    private string unlockMatrixPath;

    void Start()
    {
        unlockMatrixPath = $"{Application.persistentDataPath}/UnlockMatrix2.json";

        if (File.Exists(unlockMatrixPath))
        {
            string json = File.ReadAllText(unlockMatrixPath);
            unlockableMatrix = JsonUtility.FromJson<UnlockableMatrixx>(json);
        }

        RerenderShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHealth()
    {
        unlockableMatrix.hasHealthPerk = true;
        RerenderShop();
        SaveJson();
    }

    public void BuyCoin()
    {
        unlockableMatrix.hasCoinsPerk = true;
        RerenderShop();
        SaveJson();
    }

    public void BuyStamin()
    {
        unlockableMatrix.hasStaminaPerk = true;
        RerenderShop();
        SaveJson();
    }
    public void RerenderShop()
    {
        if (unlockableMatrix.hasHealthPerk)
        {
            healthButton.interactable = false;
        }

        if (unlockableMatrix.hasCoinsPerk)
        {
            coinButton.interactable = false;
        }

        if (unlockableMatrix.hasStaminaPerk)
        {
            staminButton.interactable = false;
        }
    }

    public void SaveJson()
    {
        string json = JsonUtility.ToJson(unlockableMatrix);
        File.WriteAllText(unlockMatrixPath, json);
    }

}
