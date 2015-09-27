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
        private Random random = new Random();
        private Logger log;
        private int turn_count = 0;
        /*
         * This generates an initial population.  
         */
        
        public Population(List<PokemonFactory> factories, int initial_population, int max_population)
        {
            this.max_population = max_population;

            random = ServiceRegistry.GetInstance().GetRandom();
            log = ServiceRegistry.GetInstance().GetLog();
            population = new List<Pokemon>();

            for (int index = 0; index < initial_population; index++)
            {
                population.Add(factories[random.Next(0, factories.Count)].Generate());
            }
        }

        public void Turn()
        {
            log.Log("Turn: " + turn_count + "\n");
            log.Log("Population: " + population.Count + "\n");
            foreach (Pokemon pokemon in population)
            {
                log.Log(pokemon + "\n");
                pokemon.Heal();
            }

            int fights_remaining = 0;

            if (population.Count > max_population)
            {
                fights_remaining = (population.Count - max_population) + (int)(population.Count * 0.10);
            }//culls to max + percent.
            else
            {
                fights_remaining = (int)(population.Count * 0.15);
            }

            int breeds_remaining = (int)(population.Count * 0.20); //introduces new pokemon to the population.
            while (fights_remaining > 0 || breeds_remaining > 0)
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
                else if(random.Next(0,2) == 0)
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

            foreach (Pokemon pokemon in population)
            {
                pokemon.IncreaseAge();
            }

            foreach (Pokemon pokemon in population.Where(p => p.Dead()).ToList<Pokemon>())
            {
                log.Log(pokemon.ToString() + " has died of old age");
                population.Remove(pokemon);
            }


            turn_count++;
        }

        private void RandomBreed()
        {
            Pokemon father = RandomPokemon();
            Pokemon mother = FindMatch(father);
            Pokemon baby = mother.Breed(father);
            log.Log("father: " + father.GetName());
            log.Log("mother: " + mother.GetName());
            log.Log("child: " + baby.GetName());
            population.Add(baby);
        }

        private Pokemon RandomPokemon()
        {
            return population[random.Next(0, population.Count)];
        }

        private Pokemon FindMatch(Pokemon source)
        {
            List<Pokemon> candidates = population.Where(candidate => candidate != source).ToList<Pokemon>();
            return candidates[random.Next(candidates.Count)];
        }

        private void RandomFight()
        {
            Pokemon attacker = RandomPokemon();
            Pokemon defender = FindMatch(attacker);
            log.Log(attacker.GetName() + " faught " + defender.GetName() + "\n");
            Fight fight = new Fight(attacker, defender);
            population.Remove(fight.Loser());
        }

        public List<Pokemon> GetPopulation()
        {
            return this.population;
        }

        public int GetTurnCount()
        {
            return turn_count;
        }

    }
}
