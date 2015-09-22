using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Population
    {
        private Dictionary<Pokemon, float> pokemon_population;

        public Population(int hp_min, int hp_max, int attack_min, int attack_max, int defense_min, int defense_max, int type, int initial_population)
        {
            Random rngesus = new Random();
            List<Pokemon> pokemon = new List<Pokemon>();
            for (int index = 0; index < initial_population; index++)
            {
                pokemon.Add(new Pokemon(type, rngesus.Next(hp_min, hp_max), rngesus.Next(attack_min, attack_max), rngesus.Next(defense_min, defense_max)));
            }
        }
    }
}
