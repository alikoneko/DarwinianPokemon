using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon.Attacks
{
    class Gust : IAttack
    {
        Random random;
        Logger log;
        public Gust()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
            log = ServiceRegistry.GetInstance().GetLog();
        }

        public int GetDamage(Pokemon attacker, Pokemon defender)
        {
            log.Log(attacker.Name + " used Gust on " + defender.Name);
            double damage = (2 * attacker.Level + 10) / 250;
            double modifier = random.NextDouble() * (1.00f - 0.85) + 0.85;
            if (random.Next(0, 1) == 0) //determines critical hit!
            {
                log.Log("Critical Hit");
                modifier *= 2;
            }
           
            damage *= (attacker.SpecialAttack) / (defender.SpecialDefense);
            damage *= 40;
            damage += 2;
            damage *= modifier;
            log.Log("Total Damage: " + damage);
            if (damage == 0)
            {
                return 1;
            }
            return (int)(Math.Round(damage));
        }


       
    }
}
