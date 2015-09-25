using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon.Attacks
{
    class Gust : Attack
    {
        Random random = new Random();
        public Gust()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        public int GetDamage(Pokemon attacker, Pokemon defender)
        {
            double damage = (2 * attacker.Level() + 10) / 250;
            double modifier = random.NextDouble() * (1.00f - 0.85) + 0.85;
            if (random.Next(0, 1) == 0) //determines critical hit!
            {
                modifier *= 2;
            }

            damage *= (attacker.GetSpecialAttack()) / (defender.GetSpecialDefense());
            damage *= 40;
            damage += 2;
            damage *= modifier;

            return (int)(Math.Round(damage));
        }
    }
}
