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
            Random rngesus = new Random();
            int hp_a = opponent_a.GetHP();
            int hp_b = opponent_b.GetHP();
            while (true)
            {
                //Console.WriteLine(" A's HP: {0}\n B's HP: {1}", hp_a, hp_b);
                if (opponent_a.GetSpeed() > opponent_b.GetSpeed())
                {
                    hp_b -= damage(opponent_a, opponent_b);
                    hp_a -= damage(opponent_b, opponent_a);
                }
                else if (opponent_a.GetSpeed() < opponent_b.GetSpeed())
                {
                    hp_a -= damage(opponent_b, opponent_a);
                    hp_b -= damage(opponent_a, opponent_b);
                }
                else
                {
                    if (rngesus.Next(0, 1) == 1)
                    {
                        hp_b -= damage(opponent_a, opponent_b);
                        hp_a -= damage(opponent_b, opponent_a);
                    }
                    else
                    {
                        hp_a -= damage(opponent_b, opponent_a);
                        hp_b -= damage(opponent_a, opponent_b);
                    }
                }
                if (hp_a <= 0)
                {
                    return opponent_b;
                }
                else if (hp_b <= 0)
                {
                    return opponent_a;
                }
            }
        }

        //From bulbapedia
        private int damage(Pokemon pokemon, Pokemon opponent)
        {
            double damage = (2 * LEVEL + 10) / 250;
            damage *= (pokemon.GetAttack() + pokemon.GetSpecialAttack()) / (pokemon.GetSpecialDefense() + pokemon.GetDefense());
            damage *= SURF;
            damage += 2;
            damage *= modifier(pokemon, opponent);
            return (int)Math.Round(damage);
        }

        //From Bulbapedia,
        // INLCUDES CRITICAL HITS!
        //TODO: TYPE EFFECTIVENESS
        private double modifier(Pokemon pokemon, Pokemon opponent)
        {
            Random rngesus = new Random();
            if (rngesus.Next(0, 1) == 0) //determines critical hit!
            {
                return rngesus.NextDouble() * (1.00f - 0.85) + 0.85;
            }
            return (rngesus.NextDouble() * (1.00f - 0.85) + 0.85) * 2;
        }
    }
}
