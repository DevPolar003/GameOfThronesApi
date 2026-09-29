using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.ApiGameOfThrones
{
    /// <summary>
    /// Gerenciador de formulário responsável por manipular dados do formulário
    /// Implementa os 4 métodos obrigatórios: setForm, getForm, ClearForm, ValideForm
    /// </summary>
    public class FormManager
    {
        private string authorName;
        private int quoteCount;
        private List<string> validationErrors;

        public FormManager()
        {
            validationErrors = new List<string>();
            ClearForm();
        }

        /// <summary>
        /// setForm() - Preenche ou configura os dados do formulário
        /// Responsável por inicializar/preencher os campos com valores
        /// </summary>
        public void setForm(string author = "", int count = 1)
        {
            this.authorName = author;
            this.quoteCount = count > 0 ? count : 1;
        }

        /// <summary>
        /// getForm() - Obtém os dados informados pelo usuário no formulário
        /// Retorna os valores configurados no formulário para serem enviados à API
        /// </summary>
        public FormData getForm()
        {
            return new FormData
            {
                AuthorName = this.authorName,
                QuoteCount = this.quoteCount
            };
        }

        /// <summary>
        /// ClearForm() - Limpa os campos do formulário
        /// Remove todos os dados e reseta para o estado inicial
        /// </summary>
        public void ClearForm()
        {
            this.authorName = "";
            this.quoteCount = 1;
            this.validationErrors.Clear();
        }

        /// <summary>
        /// ValideForm() - Valida os dados antes de realizar a chamada à API
        /// Verifica se os dados estão no formato correto
        /// Retorna true se válido, false caso contrário
        /// </summary>
        public bool ValideForm()
        {
            validationErrors.Clear();

            // Validar AuthorName
            if (string.IsNullOrWhiteSpace(authorName))
            {
                validationErrors.Add("Nome do personagem é obrigatório.");
                return false;
            }

            if (authorName.Length < 2)
            {
                validationErrors.Add("Nome do personagem deve ter no mínimo 2 caracteres.");
                return false;
            }

            // Validar QuoteCount
            if (quoteCount <= 0)
            {
                validationErrors.Add("Quantidade de frases deve ser maior que zero.");
                return false;
            }

            if (quoteCount > 10)
            {
                validationErrors.Add("Máximo de 10 frases por requisição.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Obtém a lista de erros de validação
        /// </summary>
        public List<string> GetValidationErrors()
        {
            return new List<string>(validationErrors);
        }

        /// <summary>
        /// Obtém mensagem de erro concatenada
        /// </summary>
        public string GetValidationErrorsMessage()
        {
            return string.Join("\n", validationErrors);
        }
    }

    /// <summary>
    /// Classe auxiliar para transportar dados do formulário
    /// </summary>
    public class FormData
    {
        public string AuthorName { get; set; }
        public int QuoteCount { get; set; }
    }
}
