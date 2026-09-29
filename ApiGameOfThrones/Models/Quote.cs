using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MauiApp1.ApiGameOfThrones.Models
{
    public class Quote
    {
        [JsonPropertyName("character")]
        public string Character { get; set; }

        [JsonPropertyName("sentence")]
        public string Sentence { get; set; }

        [JsonPropertyName("author")]
        public string Author { get; set; }

        public Quote() { }

        public Quote(string character, string sentence, string author)
        {
            Character = character;
            Sentence = sentence;
            Author = author;
        }

        public override string ToString()
        {
            return $"\"{Sentence}\" - {Author}";
        }
    }
}
