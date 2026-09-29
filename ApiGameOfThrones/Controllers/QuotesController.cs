using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MauiApp1.ApiGameOfThrones.Models;

namespace ApiGameOfThrones.Controllers
{
    [ApiController]
    [Route("v1")]
    public class QuotesController : ControllerBase
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

        [HttpGet("random")]
        public IActionResult GetRandom()
        {
            var q = Quotes.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            if (q == null) return NotFound();
            return Ok(q);
        }

        [HttpGet("random/{count:int}")]
        public IActionResult GetRandomCount(int count)
        {
            if (count <= 0) count = 1;
            if (count > 10) count = 10;
            var qs = Quotes.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
            return Ok(qs);
        }

        [HttpGet("author/{slug}/{count:int}")]
        public IActionResult GetByAuthor(string slug, int count)
        {
            if (string.IsNullOrEmpty(slug)) return BadRequest();
            if (count <= 0) count = 1;
            if (count > 10) count = 10;
            slug = slug.ToLower();
            var qs = Quotes.Where(q => q.Character == slug || (q.Author != null && q.Author.ToLower().Contains(slug))).Take(count).ToList();
            return Ok(qs);
        }

        [HttpGet("houses")]
        public IActionResult GetHouses() => Ok(Houses);

        [HttpGet("house/{slug}")]
        public IActionResult GetHouse(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return BadRequest();
            var house = Houses.FirstOrDefault(h => h.Slug == slug.ToLower());
            if (house == null) return NotFound();
            return Ok(house);
        }

        [HttpGet("characters")]
        public IActionResult GetCharacters() => Ok(Characters);

        [HttpGet("character/{slug}")]
        public IActionResult GetCharacter(string slug)
        {
            if (string.IsNullOrEmpty(slug)) return BadRequest();
            var character = Characters.FirstOrDefault(c => c.Slug == slug.ToLower() || c.Name.ToLower().Contains(slug.ToLower()));
            if (character == null) return NotFound();
            return Ok(character);
        }
    }
}
