using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    class Genetics
    {
        public Pokemon Breed(Pokemon mother, Pokemon father)
        {
            int average_hp = (mother.GetHP() + father.GetHP()) / 2;
            int average_atk = (mother.GetAttack() + father.GetAttack()) / 2;
            int average_def = (mother.GetDefense() + father.GetDefense()) / 2;
            int average_spa = (mother.GetSpecialAttack() + father.GetSpecialAttack()) / 2;
            int average_spdef = (mother.GetSpecialDefense() + father.GetSpecialDefense()) / 2;
            int average_spd = (mother.GetSpeed() + father.GetSpeed()) / 2;

            return new Pokemon(mother.GetType_1(), father.GetType_2(), average_hp, average_atk, average_def, average_spa, average_spdef, average_spd);
        }

        public Pokemon MutateType(Pokemon pokemon)
        {
            Random rngesus = new Random();
            if (rngesus.Next(0, 1) == 0)
            {
                return new Pokemon(rngesus.Next(0, 17), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(),
                    pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
            }
            return new Pokemon(pokemon.GetType_1(), rngesus.Next(0, 17), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(),
                pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
        }

        public Pokemon MutateStat(Pokemon pokemon)
        {
            Random rngesus = new Random();
            int switch_stat = rngesus.Next(0,5);
            int modifier = StatModifier(rngesus);

            switch (switch_stat)
            {
                case 0:
                    int hp = pokemon.GetHP() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), hp, pokemon.GetAttack(), pokemon.GetDefense(), pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
                case 1:
                    int attack = pokemon.GetAttack() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), pokemon.GetHP(), attack, pokemon.GetDefense(), pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
                case 2:
                    int defense = pokemon.GetDefense() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), defense, pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
                case 3:
                    int special_attack = pokemon.GetSpecialDefense() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(), special_attack, pokemon.GetSpecialDefense(), pokemon.GetSpeed());
                case 4:
                    int special_defense = pokemon.GetSpecialDefense() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(), pokemon.GetSpecialAttack(), special_defense, pokemon.GetSpeed());
                case 5:
                    int speed = pokemon.GetSpeed() + modifier;
                    return new Pokemon(pokemon.GetType_1(), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(), pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), speed);
                default:
                    return new Pokemon(pokemon);
            }
        }

        public Pokemon Reroll(Pokemon pokemon)
        {
            Random rngesus = new Random();
            return new Pokemon(rngesus.Next(0, 17), rngesus.Next(0, 17), rngesus.Next(pokemon.GetHP() - 30, pokemon.GetHP() + 30), rngesus.Next(pokemon.GetAttack() - 30, pokemon.GetAttack() + 30),
                rngesus.Next(pokemon.GetDefense() - 30, pokemon.GetDefense() + 30), rngesus.Next(pokemon.GetSpecialAttack() - 30, pokemon.GetSpecialAttack() + 30),
                rngesus.Next(pokemon.GetSpecialDefense() - 30, pokemon.GetSpecialDefense() + 30), rngesus.Next(pokemon.GetSpeed() - 30, pokemon.GetSpeed() + 30));
        }

        private int StatModifier(Random rngesus)
        {
            int modifier = rngesus.Next(0, 1);
            int stat = rngesus.Next(0, 5);

            if (modifier == 0)
            {
                modifier = -1;
            }

            return rngesus.Next(0, 10) * modifier;

        }

    }
}
