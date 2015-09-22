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

        public Population(int hp_min, int hp_max, int attack_min, int attack_max, int defense_min, int defense_max, int type, int initial_population)
        {
            Random rngesus = new Random(); //praise be to RNGesus, Random is random enough for this. A mersenne twister is just fine.
            population = new List<Pokemon>();
            for (int index = 0; index < initial_population; index++)
            {
                population.Add(new Pokemon(type, rngesus.Next(hp_min, hp_max), rngesus.Next(attack_min, attack_max), rngesus.Next(defense_min, defense_max)));
            }
        }
    }
}
