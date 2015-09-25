using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class StatRange
    {
        int min;
        int max;
        Random r = new Random();

        public StatRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        public int Random()
        {
            return r.Next(min, max);
        }
    }
}
