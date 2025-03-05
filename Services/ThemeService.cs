using CurrencyConverterApp.Enums;

namespace CurrencyConverterApp.Services
{
    public class ThemeService
    {
        private const string ThemeKey = "AppTheme";

        public ETheme GetTheme()
        {
            var theme = Preferences.Get(ThemeKey, ETheme.Light.ToString());
            return Enum.TryParse(theme, out ETheme parsedTheme) ? parsedTheme : ETheme.Light;
        }

        public void SetTheme(ETheme theme)
        {
            Preferences.Set(ThemeKey, theme.ToString());
            ApplyTheme(theme);
        }

        public void ApplyTheme(ETheme theme)
        {
            Application.Current.UserAppTheme = theme == ETheme.Dark ? AppTheme.Dark : AppTheme.Light;
        }
    }

}
