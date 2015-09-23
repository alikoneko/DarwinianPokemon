using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Evolution
    {
        private Dictionary<Pokemon, double> rated_population;
        private Random rngesus = new Random();

        public Evolution(Population population)
        {
            int count = 0;

            rated_population = new Dictionary<Pokemon, double>();

            List<Pokemon> unrated = population.GetPopulation();
            ShufflePopulation(unrated);
            while (count < unrated.Count)
            {
                fitness(unrated[count], unrated[count + 1]);
                count = count + 2;
            }
        }

        private void ShufflePopulation(List<Pokemon> population) 
        {
            int count = population.Count;
            Random rngesus = new Random();

            while(count > 1)
            {
                count--;
                int k = rngesus.Next(count + 1);
                Pokemon poke = population[k];
                population[k] = population[count];
                population[count] = poke;
            }
        }

        private double average_stats(Pokemon pokemon)
        {
            double average = 0.0;

            average += pokemon.GetHP();
            average += pokemon.GetAttack();
            average += pokemon.GetDefense();
            average += pokemon.GetSpecialAttack();
            average += pokemon.GetSpecialDefense();
            average += pokemon.GetSpeed();

            return average;
        }

        //arbitrarily decides fitness
        private void fitness(Pokemon pokemon_a, Pokemon pokemon_b)
        {
            Fight fight = new Fight();

            if (fight.DetermineWinner(pokemon_a, pokemon_b, rngesus) == pokemon_a)
            {
                rated_population.Add(pokemon_a, average_stats(pokemon_a)/(average_stats(pokemon_a) + average_stats(pokemon_b)));
                rated_population.Add(pokemon_b, -1 * (average_stats(pokemon_a)/(average_stats(pokemon_a) + average_stats(pokemon_b))));
            }
            else
            {
                rated_population.Add(pokemon_b, average_stats(pokemon_b) / (average_stats(pokemon_a) + average_stats(pokemon_b)));
                rated_population.Add(pokemon_a, -1 * (average_stats(pokemon_a) / (average_stats(pokemon_a) + average_stats(pokemon_b))));
            }
        }

        private void CullRandom()
        {
            KeyValuePair<Pokemon, double> culled = rated_population.ElementAt(rngesus.Next(0, rated_population.Count));
            rated_population.Remove(culled.Key);
        }
    }
}
