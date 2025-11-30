using UnityEngine;
using GoogleMobileAds.Api;

// AdMobを管理するクラス
public class AdMobMgr : MonoBehaviour
{
    private BannerView bannerView;
    private GameObject bannerObj;
    private static bool Dontdestroy = false;

    void Awake()
    {
        if(!Dontdestroy) {
            DontDestroyOnLoad(this);
            Dontdestroy = true;
        }
        else {
            Destroy(gameObject);
            return;
        }

        // Google AdMob Initial
        MobileAds.Initialize(initStatus => { RequestBanner();});
    }

    // バナーのリクエスト関数
    private void RequestBanner()
    {
        #if UNITY_ANDROID
            // string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // テスト用広告ユニットID
            string adUnitId = "ca-app-pub-3771226114317990/8467659403";    // 本番用広告ユニットID
        #elif UNITY_IPHONE
            // string adUnitId = "ca-app-pub-3940256099942544/2934735716"; // テスト用広告ユニットID
            string adUnitId = "ca-app-pub-3771226114317990/8628432151";    // 本番用広告ユニットID
        #else
            string adUnitId = "unexpected_platform";
        #endif
            if(bannerView != null) {
                bannerView.Destroy();
                bannerView = null;
            }
            // Create a 320x50 banner at the top of the screen.
            bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

            LoadBanner();
    }

    // バナーのロード関数
    private void LoadBanner()
    {
        if (bannerView == null) RequestBanner();

        // Create an empty ad request.
        AdRequest request = new AdRequest();

        // Load the banner with the request.
        bannerView.LoadAd(request);

        if(bannerObj = GameObject.Find("BANNER(Clone)")) DontDestroyOnLoad(bannerObj);
    }
}