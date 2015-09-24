using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Generation
    {
        private List<Lifetime> population;
        private Random random = new Random();

        public Generation(List<Pokemon> population)
        {
            this.population = new List<Lifetime>();

            foreach (Pokemon pokemon in population)
            {
                this.population.Add(new Lifetime(pokemon));
            }

            ShufflePopulation(this.population);

        }

        public List<Pokemon> Survivors()
        {
            List<Pokemon> survivors = new List<Pokemon>();

            population.OrderBy(f => random.Next());


            foreach (Lifetime lifetime in population)
            {
                int fight_count = 5;
                if (!lifetime.GetPokemon().Dead()) //if the pokemon is dead, move on. its dead.
                {
                    for (int i = 0; i < fight_count; i++)
                    {
                        try
                        {
                            Pokemon contestant = FindContestant(lifetime.GetPokemon());
                            lifetime.Fight(contestant);
                            lifetime.GetPokemon().Heal();
                        }
                        catch (OpponentNotFoundException e)
                        {

                        }
                    }
                }
            }

            foreach (Lifetime lifetime in population)
            {

            }

            return survivors;
        }

        private Pokemon FindContestant(Pokemon pokemon)
        {
            Pokemon contestant;

            for (int i = 0; i < 3; i++)
            {
                contestant = population[random.Next(population.Count)].GetPokemon();
                if (contestant != pokemon && !contestant.Dead()) //can't fight a dead opponent.
                {
                    return contestant;
                }
            }

            throw new OpponentNotFoundException();
        }

        private void ShufflePopulation(List<Lifetime> population) 
        {
            int count = population.Count;
            Random rngesus = new Random();

            while(count > 1)
            {
                count--;
                int k = rngesus.Next(count + 1);
                Lifetime temp = population[k];
                population[k] = population[count];
                population[count] = temp;
            }
        }

    }
}
