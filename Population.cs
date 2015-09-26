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
            log.Log("Turn: " + turn_count);
            foreach (Pokemon pokemon in population)
            {
                pokemon.Heal();
            }

            int fights_remaining = 0;

            if (population.Count > max_population)
            {
                fights_remaining = (population.Count - max_population) + (int)(population.Count * 0.10);
            }//culls to max + percent.
            else
            {
                fights_remaining = (int)(population.Count * 0.10);
            }

            int breeds_remaining = (int)(population.Count * 0.10); //introduces new pokemon to the population.
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

            turn_count++;
        }

        private void RandomBreed()
        {
            Pokemon father = RandomPokemon();
            Pokemon mother = FindMatch(father);
            Pokemon baby = mother.Breed(father);
            log.Log("father: " + father.GetName() + "/n" + "mother: " + mother.GetName() + "/n" + "child: " + baby.GetName() + "/n");
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
            log.Log(attacker.GetName() + " faught " + defender.GetName());
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
