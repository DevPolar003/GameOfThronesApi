using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MauiApp1.ApiGameOfThrones.Models;

namespace MauiApp1.ApiGameOfThrones.ConnectFactory
{
    internal static class LocalApi
    {
        private static readonly List<House> Houses = new List<House>
        {
            new House("Lannister", "lannister"),
            new House("Stark", "stark"),
            new House("Targaryen", "targaryen")
        };

        private static readonly List<Character> Characters = new List<Character>
        {
            new Character("Tyrion Lannister", "Lannister", "tyrion"),
            new Character("Jon Snow", "Stark", "jon"),
            new Character("Daenerys Targaryen", "Targaryen", "daenerys")
        };

        private static readonly List<Quote> Quotes = new List<Quote>
        {
            new Quote("tyrion", "A mind needs books like a sword needs a whetstone.", "Tyrion Lannister"),
            new Quote("tyrion", "I drink and I know things.", "Tyrion Lannister"),
            new Quote("jon", "The things I do for love.", "Jon Snow"),
            new Quote("jon", "Night gathers, and now my watch begins.", "Jon Snow"),
            new Quote("daenerys", "I will take what is mine with fire and blood.", "Daenerys Targaryen"),
            new Quote("daenerys", "I am the dragon's daughter.", "Daenerys Targaryen")
        };

        public static string ProcessEndpoint(string endpoint)
        {
            if (string.IsNullOrEmpty(endpoint)) return null;

            // Normalize
            endpoint = endpoint.Trim();
            if (endpoint.StartsWith("/")) endpoint = endpoint.Substring(1);
            if (endpoint.EndsWith("/")) endpoint = endpoint.TrimEnd('/');

            var parts = endpoint.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return null;

            try
            {
                switch (parts[0].ToLower())
                {
                    case "random":
                        return HandleRandom(parts);
                    case "author":
                        return HandleAuthor(parts);
                    case "houses":
                        return JsonSerializer.Serialize(Houses);
                    case "house":
                        return HandleHouse(parts);
                    case "characters":
                        return JsonSerializer.Serialize(Characters);
                    case "character":
                        return HandleCharacter(parts);
                    default:
                        return null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static string HandleRandom(string[] parts)
        {
            if (parts.Length == 1)
            {
                // single random
                var q = Quotes.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
                return JsonSerializer.Serialize(q);
            }

            if (parts.Length >= 2 && int.TryParse(parts[1], out int count))
            {
                if (count <= 0) count = 1;
                if (count > 10) count = 10;
                var qs = Quotes.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
                return JsonSerializer.Serialize(qs);
            }

            return null;
        }

        private static string HandleAuthor(string[] parts)
        {
            if (parts.Length < 2) return null;
            string slug = parts[1].ToLower();
            int count = 1;
            if (parts.Length >= 3) int.TryParse(parts[2], out count);
            if (count <= 0) count = 1;
            if (count > 10) count = 10;

            var qs = Quotes.Where(q => q.Character == slug || (q.Author != null && q.Author.ToLower().Contains(slug))).Take(count).ToList();
            return JsonSerializer.Serialize(qs);
        }

        private static string HandleHouse(string[] parts)
        {
            if (parts.Length < 2) return null;
            string slug = parts[1].ToLower();
            var house = Houses.FirstOrDefault(h => h.Slug == slug);
            if (house == null) return null;
            return JsonSerializer.Serialize(house);
        }

        private static string HandleCharacter(string[] parts)
        {
            if (parts.Length < 2) return null;
            string slug = parts[1].ToLower();
            var character = Characters.FirstOrDefault(c => c.Slug == slug || c.Name.ToLower().Contains(slug));
            if (character == null) return null;
            return JsonSerializer.Serialize(character);
        }
    }
}
