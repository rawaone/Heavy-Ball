using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using TMPro;
//reward ad

public class AdrewardScoreTwice : MonoBehaviour
{
	private RewardBasedVideoAd adReward;

	private string idApp, idReward;


	[SerializeField] Button BtnRewardGO;
	[SerializeField] Button BtnRewardLV;
	[SerializeField] TextMeshProUGUI TxtScoreTwiceGO;
	[SerializeField] TextMeshProUGUI TxtScoreTwiceLV;



	void Start()
	{
		//idApp = "ca-app-pub-3940256099942544~3347511713";
		idReward = "ca-app-pub-3940256099942544/5224354917";

		adReward = RewardBasedVideoAd.Instance;

		MobileAds.Initialize(InitializationStatus => { });
	}


	#region Reward video methods ---------------------------------------------

	public void RequestRewardAd()
	{
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request, idReward);

		adReward.OnAdLoaded += this.HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded += this.HandleOnAdRewarded;
		adReward.OnAdClosed += this.HandleOnRewardedAdClosed;
	}

	public void ShowRewardAd()
	{
		if (adReward.IsLoaded())
			adReward.Show();
	}
	//events
	public void HandleOnRewardedAdLoaded(object sender, EventArgs args)
	{//ad loaded
		ShowRewardAd();

	}

	public void HandleOnAdRewarded(object sender, EventArgs args)
	{
		//user finished watching ad

		//int points = int.Parse(TxtPoints.text);
		//points += 50; //add 50 points
		//TxtPoints.text = points.ToString();

		FindObjectOfType<GameManager>().AddToTotalScoreTimesTwoReward();

	}

	public void HandleOnRewardedAdClosed(object sender, EventArgs args)
	{
		//ad closed (even if not finished watching)

		BtnRewardGO.interactable = true;
		BtnRewardLV.interactable = true;
		TxtScoreTwiceGO.text = "Collect";
		TxtScoreTwiceLV.text = "Collect";

		adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= this.HandleOnAdRewarded;
		adReward.OnAdClosed -= this.HandleOnRewardedAdClosed;
	}

	#endregion

	//other functions
	//btn (more points) clicked
	public void OnGetScoreTwiceClicked()
	{
		BtnRewardGO.interactable = false;
		BtnRewardLV.interactable = false;
		TxtScoreTwiceGO.text = "Loading";
		TxtScoreTwiceLV.text = "Loading";

		RequestRewardAd();
	}

	//------------------------------------------------------------------------
	AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	void OnDestroy()
	{
		adReward.OnAdLoaded -= this.HandleOnRewardedAdLoaded;
		adReward.OnAdRewarded -= this.HandleOnAdRewarded;
		adReward.OnAdClosed -= this.HandleOnRewardedAdClosed;
	}

}