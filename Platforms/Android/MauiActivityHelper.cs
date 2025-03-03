using Android.App;

namespace CurrencyConverterApp.Platforms.Android
{
    public static class MauiActivityHelper
    {
        public static Activity? GetCurrentActivity()
        {
            return Platform.CurrentActivity;
        }
    }
}
