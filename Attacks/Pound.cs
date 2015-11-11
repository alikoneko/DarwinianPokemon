using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon.Attacks
{
    class Pound : IAttack
    {
        Random random;
        Logger log;
        public Pound()
        {
            log = ServiceRegistry.GetInstance().GetLog();
            random = ServiceRegistry.GetInstance().GetRandom();
        }

        public int GetDamage(Pokemon attacker, Pokemon defender)
        {
            log.Log(attacker.Name + " used Pound on " + defender.Name);
            double damage = (2 * attacker.Level + 10) / 250;
            double modifier = random.NextDouble() * (1.00f - 0.85) + 0.85;
            if (random.Next(0, 1) == 0) //determines critical hit!
            {
                log.Log("Critical Hit!");
                modifier *= 2;
            }
            damage *= (attacker.AttackPower) / (defender.Defense);
            damage *= 40;
            damage += 2;
            damage *= modifier;
            log.Log("Total damage: " + damage);
            if (damage == 0)
            {
                return 1;
            }
            return (int)(Math.Round(damage));
        }
    }
}
