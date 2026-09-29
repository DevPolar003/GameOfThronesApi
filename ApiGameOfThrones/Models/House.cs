using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp1.ApiGameOfThrones.Models
{
    public class House
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        public House() { }

        public House(string name, string slug)
        {
            Name = name;
            Slug = slug;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}