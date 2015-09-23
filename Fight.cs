﻿using System;
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

        private Pokemon opponent_a, opponent_b;
        private Pokemon winner, loser;

        private Random random;

        public Fight(Pokemon opponent_a, Pokemon opponent_b)
        {
            random = ServiceRegistry.GetInstance().GetRandom();
            this.opponent_a = opponent_a;
            this.opponent_b = opponent_b;
        }

        public Pokemon Winner()
        {
            if (null == winner)
            {
                RunFight();
            }
            return winner;
        }

        public Pokemon Loser()
        {
            if (null == loser)
            {
                RunFight();
            }
            return loser;
        }
        private void RunFight()
        {
            Random rngesus = new Random();

            Pokemon attacker, defender;

            if (opponent_a.GetSpeed() > opponent_b.GetSpeed())
            {
                attacker = opponent_a;
                defender = opponent_b;
            }
            else if (opponent_b.GetSpeed() > opponent_a.GetSpeed())
            {
                attacker = opponent_b;
                defender = opponent_a;
            }
            else
            {
                if (rngesus.Next(0, 1) == 1)
                {
                    attacker = opponent_a;
                    defender = opponent_b;
                }
                else
                {
                    attacker = opponent_b;
                    defender = opponent_a;
                }
            }

            while (true)
            {
                attacker.Attack(defender);

                if(defender.Dead())
                {
                    winner = attacker;
                    loser = defender;
                    return;
                }

                Pokemon temp = attacker;
                attacker = defender;
                defender = temp;
            }
        }
    }
}
