using System;
using System.Collections.Generic;
using System.Text;

namespace MauiApp1.ApiGameOfThrones.Models
{
    public class House
    {
        private string name;
        private string slug;

        public House(string name, string slug)
        {
            name = name;
            slug = slug;
        }
    }
}
