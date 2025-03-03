using CurrencyConverterApp.Services.Interface;

#if ANDROID
using CurrencyConverterApp.Platforms.Android;
#endif

namespace CurrencyConverterApp.Services
{
    public class AppCloser : IAppCloser
    {
        public void Close()
        {
#if ANDROID
            var activity = MauiActivityHelper.GetCurrentActivity();
            activity?.FinishAffinity();
#endif
        }
    }
}
