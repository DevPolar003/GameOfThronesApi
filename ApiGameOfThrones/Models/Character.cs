using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp1.ApiGameOfThrones.Models
{
    public class Character
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("house")]
        public string House { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        public Character() { }

        public Character(string name, string house, string slug)
        {
            Name = name;
            House = house;
            Slug = slug;
        }

        public override string ToString()
        {
            return $"{Name} - Casa {House}";
        }
    }
}