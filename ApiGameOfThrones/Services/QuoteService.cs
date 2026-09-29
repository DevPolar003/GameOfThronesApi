using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using MauiApp1.ApiGameOfThrones.ConnectFactory;
using MauiApp1.ApiGameOfThrones.Models;

namespace MauiApp1.ApiGameOfThrones.Services
{
    public class QuoteService
    {
        private ConnectFactory.ConnectFactory connectFactory;

        public QuoteService()
        {
            connectFactory = new ConnectFactory.ConnectFactory();
        }

        /// <summary>
        /// Obtém uma frase aleatória
        /// </summary>
        public async Task<Quote> GetRandomQuote()
        {
            string json = await connectFactory.GetAsync("/random");
            if (json == null) return null;

            return connectFactory.DeserializeJson<Quote>(json);
        }

        /// <summary>
        /// Obtém múltiplas frases aleatórias
        /// </summary>
        public async Task<List<Quote>> GetRandomQuotes(int count)
        {
            if (count <= 0) count = 1;
            if (count > 10) count = 10;

            string json = await connectFactory.GetAsync($"/random/{count}");
            if (json == null) return null;

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Quote>>(json, options);
        }

        /// <summary>
        /// Obtém frases de um personagem específico
        /// </summary>
        public async Task<List<Quote>> GetQuotesByAuthor(string author, int count = 5)
        {
            if (string.IsNullOrEmpty(author)) return null;
            if (count <= 0) count = 1;
            if (count > 10) count = 10;

            string slug = author.ToLower().Replace(" ", "-");
            string json = await connectFactory.GetAsync($"/author/{slug}/{count}");
            if (json == null) return null;

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Quote>>(json, options);
        }

        /// <summary>
        /// Obtém todos os personagens
        /// </summary>
        public async Task<List<Character>> GetAllCharacters()
        {
            string json = await connectFactory.GetAsync("/characters");
            if (json == null) return null;

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<Character>>(json, options);
        }

        /// <summary>
        /// Obtém um personagem específico
        /// </summary>
        public async Task<Character> GetCharacter(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            string slug = name.ToLower().Replace(" ", "-");
            string json = await connectFactory.GetAsync($"/character/{slug}");
            if (json == null) return null;

            return connectFactory.DeserializeJson<Character>(json);
        }

        /// <summary>
        /// Obtém todas as casas
        /// </summary>
        public async Task<List<House>> GetAllHouses()
        {
            string json = await connectFactory.GetAsync("/houses");
            if (json == null) return null;

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<House>>(json, options);
        }

        /// <summary>
        /// Obtém uma casa específica
        /// </summary>
        public async Task<House> GetHouse(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            string slug = name.ToLower().Replace(" ", "-");
            string json = await connectFactory.GetAsync($"/house/{slug}");
            if (json == null) return null;

            return connectFactory.DeserializeJson<House>(json);
        }
    }
}
