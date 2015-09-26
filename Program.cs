﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DarwinianPokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            int initial_population = 50;
            int max_population = 1000;
            PokemonFactoryRepository pokemon_factory_repository = new PokemonFactoryRepository();
            Population population = new Population(pokemon_factory_repository.All(), initial_population, max_population);
            //foreach(Pokemon pokemon in population.GetPopulation())
            //{
            //    Console.WriteLine(pokemon);
            //}
            for (int turn = 0; turn < 1500; turn++)
            {
                population.Turn();
                System.Threading.Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }
}
