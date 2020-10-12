using EfCoreApp.Data;
using EfCoreApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SamuraiContext();

            var samurais = context.Samurais.ToList();
            var clans = context.Clans.ToList();
            //var sam = new Samurai
            //{
            //    Name = "Joe",
            //    Clan = new Clan { ClanName = "The Best" },
            //    Horse = new Horse { Name = "Horsey"},
            //    Quotes = new List<Quote>() { new Quote { Text ="This is awesome!"} }
            //};

            //context.Add(sam);
            //context.SaveChanges();
            
            Console.WriteLine("Hello World!");
        }
    }
}
