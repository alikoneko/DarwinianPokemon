using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon.Attacks
{
    class HyperVoice : IAttack
    {
        Random random ;

        public HyperVoice()
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

            damage *= (attacker.GetAttack()) / (defender.GetDefense());
            damage *= 90;
            damage += 2;
            damage *= modifier;

            return (int)(Math.Round(damage));
        }
    }
}
