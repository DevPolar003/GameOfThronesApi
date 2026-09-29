using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace MauiApp1.ApiGameOfThrones.ConnectFactory
{
    public class ConnectFactory
    {
        private static readonly string API_BASE_URL = "https://api.gameofthronesquotes.xyz/v1";
        private HttpClient httpClient;

        public ConnectFactory()
        {
            httpClient = new HttpClient();
        }

        /// <summary>
        /// Realiza uma requisição GET para a API
        /// </summary>
        /// <param name="endpoint">Endpoint da API (ex: "/random", "/characters")</param>
        /// <returns>Resposta em string JSON ou null se houver erro</returns>
        public async Task<string> GetAsync(string endpoint)
        {
            try
            {
                string url = $"{API_BASE_URL}{endpoint}";
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro na requisição: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Deserializa uma string JSON para o tipo especificado
        /// </summary>
        public T DeserializeJson<T>(string json) where T : class
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao deserializar JSON: {ex.Message}");
                return null;
            }
        }
    }
}
