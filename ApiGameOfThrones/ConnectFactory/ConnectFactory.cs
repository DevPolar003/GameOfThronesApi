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
        // The original implementation called the external API at api.gameofthronesquotes.xyz.
        // For this exercise we provide a local in-memory implementation of the API routes
        // defined in the repository README. GetAsync routes the request to LocalApi.

        /// <summary>
        /// Realiza uma requisição GET para a API (local implementation)
        /// </summary>
        /// <param name="endpoint">Endpoint da API (ex: "/random", "/characters")</param>
        /// <returns>Resposta em string JSON ou null se houver erro</returns>
        public async Task<string> GetAsync(string endpoint)
        {
            try
            {
                // Delegate to the in-process LocalApi to return JSON for known endpoints.
                return await Task.FromResult(LocalApi.ProcessEndpoint(endpoint));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro na requisição local: {ex.Message}");
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
