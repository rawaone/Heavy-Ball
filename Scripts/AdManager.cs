using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using TMPro;

public class AdManager : MonoBehaviour
{

    private BannerView bannerAd;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;
    static bool bannerAdRequested = false;

    public static AdManager instance;
    bool isRewarded = false;
    //[SerializeField] public TextMeshProUGUI totalScoreTextOnShop;

    [SerializeField] bool isGetScoreShopReward = false;
    [SerializeField] bool isGetScoreTimeTwo = false;
    [SerializeField] bool isGetCheckPoint = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
        this.rewardBasedVideo.OnAdRewarded += this.HandleRewardBasedVideoRewarded;
        this.rewardBasedVideo.OnAdClosed += this.HandleRewardBasedVideoClosed;

        this.RequestRewardBasedVideo();

        this.RequestBanner();
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        //AdRequest request = new AdRequest.Builder().Build();
        this.bannerAd.LoadAd(this.CreateAdRequest());
    }

    public void RequestInterstitial()
    {

        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
            this.interstitial.Destroy();

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            Debug.Log("Inerstitial Ad is not ready yet");
        }
    }

    public void RequestRewardBasedVideo()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

        this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), adUnitId);
    }
    public void ShowRewardBasedVideo()
    {
        if (this.rewardBasedVideo.IsLoaded())
        {
            this.rewardBasedVideo.Show();
        }
    }

    #region RewardBasedVideo callback handlers

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        this.RequestRewardBasedVideo();
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        isRewarded = true;
    }

    #endregion

    void Update()
    {
        if (isRewarded)
        {
            isRewarded = false;

            //FindObjectOfType<GameManager>().AddScoreReward();
            //totalScoreTextOnShop.text = FindObjectOfType<GameManager>().totalScore.ToString();

            //if (isGetScoreShopReward == true)
            //{
            //    FindObjectOfType<GameManager>().AddScoreReward();
            //    totalScoreTextOnShop.text = FindObjectOfType<GameManager>().totalScore.ToString();
            //}
            //else if (isGetScoreTimeTwo == true)
            //{
            //    FindObjectOfType<GameManager>().AddToTotalScoreTimesTwoReward();
            //}
            //else if (isGetCheckPoint == true)
            //{
            //    FindObjectOfType<GameManager>().SaveMe();
            //}
        }
    }


    public void GetScoreShopReward()
    {
        isGetScoreShopReward = true;
    }

    public void GetScoreTimeTwo()
    {
        isGetScoreTimeTwo = true;
    }
    public void GetCheckPoint()
    {
        isGetCheckPoint = true;
    }
}
