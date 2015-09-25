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
                pokemon.Heal();
            }
            
            int fights_remaining = (population.Count - max_population) + (int)(population.Count * 0.10); //culls to max + percent.
            int breeds_remaining = (int)(population.Count * 0.15); //introduces new pokemon to the population.

            while (fights_remaining > 0 && breeds_remaining > 0)
            {
                if (breeds_remaining == 0)
                {
                    RandomFight();
                    fights_remaining--;
                }
                else if (fights_remaining == 0)
                {
                    RandomBreed();
                    breeds_remaining--;
                }
                else if(random.Next(0,1) == 0)
                {
                    RandomFight();
                    fights_remaining--;
                }
                else
                {
                    RandomBreed();
                    breeds_remaining--;
                }
            }
        }

        private void RandomBreed()
        {
            Pokemon father = RandomPokemon();
            Pokemon mother = FindMatch(father);
            Pokemon baby = mother.Breed(father);
            population.Add(baby);
        }

        private Pokemon RandomPokemon()
        {
            return population[random.Next(0, population.Count)];
        }

        private Pokemon FindMatch(Pokemon source)
        {
            List<Pokemon> candidates = (List<Pokemon>)(population.Where(candidate => candidate != source));
            return candidates[random.Next(candidates.Count)];
        }

        private void RandomFight()
        {
            Pokemon attacker = RandomPokemon();
            Pokemon defender = FindMatch(attacker);
            Fight fight = new Fight(attacker, defender);
            population.Remove(fight.Loser());
        }

        public List<Pokemon> GetPopulation()
        {
            return this.population;
        }

    }
}
