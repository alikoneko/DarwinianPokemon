using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Population
    {
        private List<Pokemon> population;
        private int max_population;
        Random random = new Random();
        /*
         * This generates an initial population.  
         */
        public Population(PokemonFactory pokemon_factory, int initial_population)
        {
            Initialize();

            for (int index = 0; index < initial_population; index++)
            {
                population.Add(pokemon_factory.Generate());
            }
        }

        public Population(List<PokemonFactory> factories, int initial_population)
        {
            Initialize();
            for (int index = 0; index < initial_population; index++)
            {
                population.Add(factories[random.Next(0, factories.Count)].Generate());
            }
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
            population = new List<Pokemon>();
        }
        public void Turn()
        {
            foreach (Pokemon pokemon in population)
            {
                pokemon.max_heal();
            }
            
            int fights = (population.Count - max_population) + (int)(population.Count * 0.30); //culls to max + percent.
            int breed = (int)(population.Count * 0.50); //introduces new pokemon to the population.

            while (fights > 0 && breed > 0)
            {
                Fight fight = new Fight(population[random.Next(0, population.Count)], population[random.Next(0, population.Count)]);
                population.Remove(fight.Loser());
                int mutation = random.Next(0, 3);
                Pokemon baby = population[random.Next(0, population.Count)].Breed(population[random.Next(0, population.Count)]);
                switch (mutation)
                {
                    case 0:
                        baby = baby.MutateStat();
                        break;
                    case 1:
                        baby = baby.MutateType();
                        break;
                    case 2:
                        baby = baby.Reroll();
                        break;
                    default:
                        break;
                }
                population.Add(baby);
                fights++;
                breed++;
            }
        }

        public List<Pokemon> GetPopulation()
        {
            return this.population;
        }

    }
}
