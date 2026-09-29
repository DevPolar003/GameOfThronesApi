using Android.App;
using System;
using System.Collections.Generic;
using System.Text;


namespace MauiApp1.ApiGameOfThrones.Models
{
    public class Character
    {
        private string name;
        private string house;
        private string slug;

        public Character(string name, string house, string slug)
        {
            name = name;
            house = house;
            slug = slug;
        }
    }   
            
        }
       