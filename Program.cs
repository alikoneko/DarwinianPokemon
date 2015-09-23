using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            Pokemon one = new Pokemon(0,0, 115, 54, 49, 45, 63, 54);
            Pokemon two = new Pokemon(0, 0, 162, 117, 112, 106, 128, 117);

            Fight fight = new Fight(one, two);

            Console.WriteLine(fight.Winner());
            Console.WriteLine(fight.Loser());

            Console.ReadKey();
        }
    }
}
