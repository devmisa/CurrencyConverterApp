using CurrencyConverterApp.Enums;
using CurrencyConverterApp.Services;
using CurrencyConverterApp.Services.Interface;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CurrencyConverterApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ICurrencyService _currencyService;
        private readonly ThemeService _themeService;
        private string _valorEntrada = string.Empty;
        private string _moedaOrigem = string.Empty;
        private string _moedaDestino = string.Empty ;
        private string _resultado = string.Empty;
        private bool _isBusy;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> Currency { get; } = ["USD", "BRL", "EUR", "GBP", "JPY"];

        public string ValorEntrada
        {
            get => _valorEntrada;
            set { _valorEntrada = value; OnPropertyChanged(); }
        }

        public string MoedaOrigem
        {
            get => _moedaOrigem;
            set { _moedaOrigem = value; OnPropertyChanged(); }
        }

        public string MoedaDestino
        {
            get => _moedaDestino;
            set { _moedaDestino = value; OnPropertyChanged(); }
        }

        public string Resultado
        {
            get => _resultado;
            set { _resultado = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public ICommand ConverterCommand { get; }
        public ICommand SwapMoedasCommand { get; }
        public ICommand ToggleThemeCommand { get; }



        public MainViewModel(ICurrencyService currencyService, ThemeService themeService)
        {
            _currencyService = currencyService;
            MoedaOrigem = Currency.FirstOrDefault() ?? string.Empty;
            MoedaDestino = Currency.Skip(1).FirstOrDefault() ?? string.Empty;
            ConverterCommand = new Command(async () => await CurrencyConverterAsync(), () => !IsBusy);
            SwapMoedasCommand = new Command(SwapCurrency);
            _themeService = themeService;
            ToggleThemeCommand = new Command(ToggleTheme);
        }

        private async Task CurrencyConverterAsync()
        {
            var mainPage = Application.Current?.Windows[0].Page;

            if (MoedaOrigem == MoedaDestino)
            {
                await mainPage.DisplayAlert("Atenção!", "As moedas de origem e destino devem ser diferentes", "Fechar");
                return;
            }

            if (string.IsNullOrWhiteSpace(ValorEntrada) || !decimal.TryParse(ValorEntrada, out decimal valor))
            {
                await mainPage.DisplayAlert("Atenção!", "Valor inválido", "Fechar");
                return;
            }

            if (string.IsNullOrEmpty(MoedaOrigem) || string.IsNullOrEmpty(MoedaDestino))
            {
                await mainPage.DisplayAlert("Atenção!", "Selecione as moedas!", "Fechar");
                return;
            }

            try
            {
                IsBusy = true;
                decimal taxa = await _currencyService.GetExchangeRateAsync(MoedaOrigem, MoedaDestino);

                if (taxa > 0)
                {
                    decimal resultado = valor * taxa;
                    Resultado = $"{valor} {MoedaOrigem} = {resultado:F2} {MoedaDestino}";
                }
                else
                {
                    await mainPage.DisplayAlert("Ops!", "Conversão indisponível", "Fechar");
                }
            }
            catch (ApplicationException ex)
            {
                await mainPage.DisplayAlert("Ops!", $"Ocorreu um erro: {ex.Message}", "Fechar");
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SwapCurrency()
        {
            if (!string.IsNullOrEmpty(MoedaOrigem) && !string.IsNullOrEmpty(MoedaDestino))
            {
                (MoedaOrigem, MoedaDestino) = (MoedaDestino, MoedaOrigem);
            }
        }

        private void ToggleTheme()
        {
            var newTheme = _themeService.GetTheme() == ETheme.Light ? ETheme.Dark : ETheme.Light;
            _themeService.SetTheme(newTheme);
        }
    }
}

