using CurrencyConverterApp.Services.Interface;
using CurrencyConverterApp.ViewModels;


namespace CurrencyConverterApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IAppCloser _appCloser;

        public MainPage(MainViewModel viewModel, IAppCloser appCloser)
        {
            InitializeComponent();
            BindingContext = viewModel;
            _appCloser = appCloser;
        }

        private void OnCloseButtonClicked(object sender, EventArgs e)
        {
            _appCloser.Close();
        }
    }
}
