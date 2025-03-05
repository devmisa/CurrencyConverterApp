using CurrencyConverterApp.Services;

namespace CurrencyConverterApp
{
    public partial class App : Application
    {

        private readonly ThemeService _themeService;
        private readonly MainPage _mainPage;

        public App(ThemeService themeService, MainPage mainPage)
        {
            InitializeComponent();

            _themeService = themeService;
            _mainPage = mainPage;

            var currentTheme = themeService.GetTheme();
            themeService.ApplyTheme(currentTheme);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}