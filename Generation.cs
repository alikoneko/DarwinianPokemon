using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Generation
    {
        private List<Pokemon> population;
        private Random random = new Random();

        public Generation(List<Pokemon> population)
        {
            this.population = population;
        }

        public List<Pokemon> Survivors()
        {
            List<Pokemon> survivors = new List<Pokemon>();

            population.OrderBy(f => random.Next());


            foreach(Pokemon pokemon in population)
            {
                int fights_won = 0;
                int fight_count = 5;

                for(int i = 0; i < fight_count; i++) {
                    Pokemon contestant = FindContestant(pokemon);
                    Fight fight = new Fight(pokemon, contestant);
                }
            }

            return survivors;
        }

        private Pokemon FindContestant(Pokemon pokemon) {
            Pokemon contestant;

            for (int i = 0; i < 3; i++)
            {
                contestant = population[random.Next(population.Count)];
                if (contestant != pokemon)
                {
                    return contestant;
                }
            }

            // throw an error if contestant is null here.
            return null;
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

        ////arbitrarily decides fitness
        //private void fitness(Pokemon pokemon_a, Pokemon pokemon_b)
        //{
        //    if (fight.DetermineWinner(pokemon_a, pokemon_b, rngesus) == pokemon_a)
        //    {
        //        rated_population.Add(pokemon_a, average_stats(pokemon_a)/(average_stats(pokemon_a) + average_stats(pokemon_b)));
        //        rated_population.Add(pokemon_b, -1 * (average_stats(pokemon_a)/(average_stats(pokemon_a) + average_stats(pokemon_b))));
        //    }
        //    else
        //    {
        //        rated_population.Add(pokemon_b, average_stats(pokemon_b) / (average_stats(pokemon_a) + average_stats(pokemon_b)));
        //        rated_population.Add(pokemon_a, -1 * (average_stats(pokemon_a) / (average_stats(pokemon_a) + average_stats(pokemon_b))));
        //    }
        //}

        //private void CullRandom()
        //{
        //    KeyValuePair<Pokemon, double> culled = rated_population.ElementAt(rngesus.Next(0, rated_population.Count));
        //    rated_population.Remove(culled.Key);
        //}

        //private void CullBottom()
        //{
        //    rated_population.OrderBy(x => x.Value);

        //}
    }
}
