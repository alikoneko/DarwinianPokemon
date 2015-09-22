using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Fight
    {
        private const int LEVEL = 50; // pulling stat ranges from the level 50 pokedex
        private const int SURF = 90; // this is a 90 power move ALL pokemon learn one 90 power move. This was selected as a good base

        public Pokemon DetermineWinner(Pokemon opponent_a, Pokemon opponent_b)
        {
            int hp_a = opponent_a.hp;
            int hp_b = opponent_b.hp;

        }

        private int damage(Pokemon pokemon)
        {
            int damage = (2 * LEVEL + 10) / 250;
            damage *= (pokemon.attack + pokemon.special_attack) / (pokemon.special_defense + pokemon.defense);
            damage *= SURF;
            damage += 2;
            //damage *= modifier(pokemon);
            return damage;
        }
    }
}
