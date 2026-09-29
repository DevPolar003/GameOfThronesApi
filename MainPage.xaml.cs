using MauiApp1.ApiGameOfThrones;
using MauiApp1.ApiGameOfThrones.Services;
using MauiApp1.ApiGameOfThrones.Models;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private FormManager formManager;
        private QuoteService quoteService;

        public MainPage()
        {
            InitializeComponent();
            formManager = new FormManager();
            quoteService = new QuoteService();
        }

        /// <summary>
        /// Evento ao clicar no botão Buscar
        /// Valida o formulário e busca frases da API
        /// </summary>
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            try
            {
                // Obter dados do formulário
                string author = AuthorEntry.Text;
                string countText = CountEntry.Text;

                // Validar entrada de quantidade
                if (!int.TryParse(countText, out int count))
                {
                    ShowError("Por favor, insira um número válido para a quantidade de frases.");
                    return;
                }

                // Preencher o formulário (setForm)
                formManager.setForm(author, count);

                // Validar o formulário (ValideForm)
                if (!formManager.ValideForm())
                {
                    string errorMessage = formManager.GetValidationErrorsMessage();
                    ShowError(errorMessage);
                    return;
                }

                // Obter dados validados (getForm)
                var formData = formManager.getForm();

                // Mostrar indicador de carregamento
                ShowLoading(true);
                HideError();

                // Buscar frases da API
                var quotes = await quoteService.GetQuotesByAuthor(formData.AuthorName, formData.QuoteCount);

                // Verificar se recebeu dados
                if (quotes == null || quotes.Count == 0)
                {
                    ShowError($"Nenhuma frase encontrada para '{formData.AuthorName}'. Verifique o nome e tente novamente.");
                    ShowLoading(false);
                    return;
                }

                // Exibir resultados
                DisplayResults(quotes);
                ShowLoading(false);
            }
            catch (Exception ex)
            {
                ShowLoading(false);
                ShowError($"Erro ao buscar frases: {ex.Message}");
            }
        }

        /// <summary>
        /// Evento ao clicar no botão Limpar
        /// Limpa o formulário e os resultados (ClearForm)
        /// </summary>
        private void OnClearClicked(object sender, EventArgs e)
        {
            // Limpar o formulário (ClearForm)
            formManager.ClearForm();

            // Limpar campos de entrada
            AuthorEntry.Text = "";
            CountEntry.Text = "5";

            // Limpar resultados
            HideResults();
            HideError();
            ShowLoading(false);
        }

        /// <summary>
        /// Exibe os resultados na tela
        /// </summary>
        private void DisplayResults(List<Quote> quotes)
        {
            QuotesCollectionView.ItemsSource = quotes;
            ResultsFrame.IsVisible = true;
        }

        /// <summary>
        /// Esconde a seção de resultados
        /// </summary>
        private void HideResults()
        {
            ResultsFrame.IsVisible = false;
            QuotesCollectionView.ItemsSource = null;
        }

        /// <summary>
        /// Exibe mensagem de erro
        /// </summary>
        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }

        /// <summary>
        /// Esconde mensagem de erro
        /// </summary>
        private void HideError()
        {
            ErrorLabel.IsVisible = false;
            ErrorLabel.Text = "";
        }

        /// <summary>
        /// Mostra ou esconde o indicador de carregamento
        /// </summary>
        private void ShowLoading(bool isLoading)
        {
            LoadingIndicator.IsRunning = isLoading;
            LoadingIndicator.IsVisible = isLoading;
            SearchButton.IsEnabled = !isLoading;
        }
    }
}

