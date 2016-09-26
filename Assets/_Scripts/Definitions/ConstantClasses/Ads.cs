namespace _Scripts.Definitions.ConstantClasses
{
    public static class Ads
    {
        public const string ConversionKey = "";
        public const string AdvertisingId = "";

#if DEBUG
        public const string PackageName = "net.yaraee.freefall.debug";
#elif UNITY_ANDROID
        public const string PackageName = "net.yaraee.freefall";
#elif UNITY_IOS
        public const string PackageName = "net.yaraee.freefall";
#endif
    }
}