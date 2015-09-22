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
        /*
         * This generates an initial population.  
         */
        public Population(int type_1, int type_2, int hp_min, int hp_max, 
            int attack_min, int attack_max, int defense_min, int defense_max, 
            int special_attack_min, int special_attack_max, int special_defense_min, int special_defense_max, 
            int speed_min, int speed_max, int initial_population)
        {
            Random rngesus = new Random(); //praise be to RNGesus, Random is random enough for this. A mersenne twister is just fine.
            population = new List<Pokemon>();
            for (int index = 0; index < initial_population; index++)
            {
                population.Add(new Pokemon(type_1, type_2, rngesus.Next(hp_min, hp_max), rngesus.Next(attack_min, attack_max), rngesus.Next(defense_min, defense_max), 
                    rngesus.Next(special_attack_min, special_attack_max), rngesus.Next(special_defense_min, special_defense_max), rngesus.Next(speed_min, speed_max)));
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
    }
}
