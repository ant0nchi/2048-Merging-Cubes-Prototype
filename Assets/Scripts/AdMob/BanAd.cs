using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

public class BanAd : MonoBehaviour
{
    private BannerView bannerView;
    private string bannerUnitId = "ca-app-pub-3940256099942544/6300978111";

    void OnEnable()
    {
        bannerView = new BannerView(bannerUnitId, AdSize.Banner, AdPosition.Top);
        AdRequest adRequest = new AdRequest.Builder().Build();
        bannerView.LoadAd(adRequest);
    }

    IEnumerator ShowBanner()
    {
        yield return new WaitForSeconds(1);
        bannerView.Show();
    }
}
