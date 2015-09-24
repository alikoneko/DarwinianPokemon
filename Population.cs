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
        public Population(int type_1, int type_2, int hp_min, int hp_max, 
            int attack_min, int attack_max, int defense_min, int defense_max, 
            int special_attack_min, int special_attack_max, int special_defense_min, int special_defense_max, 
            int speed_min, int speed_max, int initial_population)
        {
            Random random = new Random(); //praise be to RNGesus, Random is random enough for this. A mersenne twister is just fine. seed later?
            population = new List<Pokemon>();
            for (int index = 0; index < initial_population; index++)
            {
                population.Add(new Pokemon(type_1, type_2, random.Next(hp_min, hp_max), random.Next(attack_min, attack_max), random.Next(defense_min, defense_max), 
                    random.Next(special_attack_min, special_attack_max), random.Next(special_defense_min, special_defense_max), random.Next(speed_min, speed_max)));
            }
        }

        public Population(List<Pokemon> population)
        {
            this.population = new List<Pokemon>();
            foreach (Pokemon pokemon in population)
            {
                this.population.Add(pokemon);
            }
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
