using CurrencyConverterApp.Services;

namespace CurrencyConverterApp
{
    public partial class App : Application
    {

        private readonly ThemeService _themeService;

        public App(ThemeService themeService)
        {
            InitializeComponent();

            _themeService = themeService;

            var currentTheme = _themeService.GetTheme();
            _themeService.ApplyTheme(currentTheme);

            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);

            window.Width = 400;
            window.Height = 800;

            return window;
        }
    }
}