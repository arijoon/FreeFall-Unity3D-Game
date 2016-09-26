using TuneSDK;
using UnityEngine;
using _Scripts.Definitions.ConstantClasses;

namespace _Scripts.Advertising.InstallTracking
{
    public partial class InstallTracker : MonoBehaviour
    {
#if UNITY_ANDROID || UNITY_IOS
        void Awake()
        {
            Tune.Init(Ads.AdvertisingId, Ads.ConversionKey);

            Tune.SetPackageName(Ads.PackageName);
            // Check if a deferred deep link is available and handle opening of the deep link as appropriate in the success callback.
            // Uncomment the following line if your MAT account has enabled deferred deep links
            Tune.CheckForDeferredDeeplink();

            // Uncomment the following line to enable auto-measurement of successful in-app-purchase (IAP) transactions as "purchase" events
            // Tune.AutomateIapEventMeasurement(true);

            // If you have existing users from before TUNE SDK implementation,
            // identify those users using this code snippet.
            
            if (PlayerPrefs.HasKey(SaveKeys.MaxBonus))
                Tune.SetExistingUser(true);
            else
                PlayerPrefs.SetInt(SaveKeys.ExistingUser, 1);

            // Measure initial app open
            Tune.MeasureSession();
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                // Measure app resumes from background
                Tune.MeasureSession();
            }
        }

#endif
    }
}
