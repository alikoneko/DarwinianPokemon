using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Lifetime
    {
        private int kills;
        private int deaths;
        Pokemon pokemon;

        public Lifetime(Pokemon pokemon)
        {
            this.pokemon = pokemon;
            kills = 0;
            deaths = 0;
        }

        public void Fight(Pokemon contestant)
        {
            Fight fight = new Fight(pokemon, contestant);
            if (fight.Winner() == pokemon)
            {
                kills++;
            }
            else
            {
                deaths++;
            }
        }
        public double GetKDR()
        {
            return (double) kills / (kills + deaths);
        }

    }
}
