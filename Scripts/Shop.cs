using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public Sprite image;
        public int price;
        public bool isPurchased = false;
        public bool isEquiped = false;
        public int isPurActive;
    }

    [SerializeField] List<ShopItem> shopItemList;
    [SerializeField] TextMeshProUGUI coinsText;

    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform shopScrollView;
    Button buyBtn;
    Button equipBtn;

    public GameObject notEnoughMoneyPanel;
    public GameObject notPurchasedPanel;
    public AudioSource messageAudio;

    public GameObject shopPanelObj;
    public GameObject settingMenuObj;
    public GameObject currentLeveltext;
    public GameObject tapToPlaytext;
    public GameObject startMenuPanelObj;

    public void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        

        int len = shopItemList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = shopItemList[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = shopItemList[i].price.ToString();
            buyBtn = g.transform.GetChild(1).GetComponent<Button>();
            buyBtn.interactable = !shopItemList[i].isPurchased; //agat lama bet
            buyBtn.AddEventListener(i, OnShopItemBtnClicked);

            shopItemList[i].isPurActive = PlayerPrefs.GetInt("Purchaseditem" + i);
            
            
            if (shopItemList[i].isPurActive == 1)
            {
                shopItemList[i].isPurchased = true;
                DisableBuyButton();
                
            }


            //Equip part
            equipBtn = g.transform.GetChild(0).GetComponent<Button>();
            //equipBtn.interactable = !shopItemList[i].isEquiped;
            equipBtn.AddEventListener(i, OnEquipItemBtnClicked);

            
        }

            Destroy(itemTemplate);

            SetCoinsUI();



        }


    void OnShopItemBtnClicked(int itemIndex)
        {
            //Debug.Log(itemIndex);

            if (Game.Instance.HasEnoughCoins(shopItemList[itemIndex].price))
            {
                Game.Instance.UseCoins(shopItemList[itemIndex].price);
                //purchase items
                //shopItemList[itemIndex].isPurchased = true;
                shopItemList[itemIndex].isPurActive = 1;

                //Showing the equip button
                //equipBtn = shopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
                //equipBtn.gameObject.SetActive(true);

                //disable the button
                buyBtn = shopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<Button>();
                DisableBuyButton();

                FindObjectOfType<GameManager>().SaveTotalScore();


            OnEquipItemBtnClicked(itemIndex);

                SetCoinsUI();

                
            }
            else
            {
                notEnoughMoneyPanel.SetActive(true);
            messageAudio.Play();
                Debug.Log("You don't have enough money!");
            }

            if (shopItemList[itemIndex].isPurActive == 1)
            {
                shopItemList[itemIndex].isPurchased = true;
                PlayerPrefs.SetInt("Purchaseditem" + itemIndex, shopItemList[itemIndex].isPurActive);
                Debug.Log("Saved");
                
        }

        
        

        }

        void SetCoinsUI()
        {
            coinsText.text = FindObjectOfType<GameManager>().totalScore.ToString();
        }


        void OnEquipItemBtnClicked(int itemIndex)
        {
           //Debug.Log(itemIndex);

            if (shopItemList[itemIndex].isPurchased)
            {
            //equip items
            shopItemList[itemIndex].isEquiped = true;


            //select the character
            //FindObjectOfType<CharachterSelector>().ChangeCharacter(itemIndex);
            ChangeCharacterOnStart(itemIndex);

            //disable the equip button
            equipBtn = shopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Button>();
            //equipBtn.interactable = false;
            //equipBtn.transform.GetChild(0).GetComponent<Text>().text = "Equiped";

            CloseShop();

            }
            else
            {
            notPurchasedPanel.SetActive(true);
            messageAudio.Play();
                Debug.Log("you haven't purchased this!");
            }

        


    }


    public void ChangeCharacterOnStart(int itemIndex)
    {
        FindObjectOfType<CharachterSelector>().ChangeCharacter(itemIndex);

    }


    private void DisableBuyButton()
    {
        buyBtn.gameObject.SetActive(false);
        buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
        buyBtn.transform.GetChild(1).gameObject.SetActive(false);
        //buyBtn.interactable = false;

        
    }

    private void CloseShop()
    {
        FindObjectOfType<AdManager>().RequestInterstitial();
        FindObjectOfType<AdManager>().ShowInterstitial();

        shopPanelObj.gameObject.SetActive(false);
        startMenuPanelObj.gameObject.SetActive(true);
        settingMenuObj.gameObject.SetActive(true);
        currentLeveltext.gameObject.SetActive(true);
        tapToPlaytext.gameObject.SetActive(true);
    }
}

