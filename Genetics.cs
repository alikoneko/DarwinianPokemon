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
            int average_hp = (mother.hp + father.hp) / 2;
            int average_atk = (mother.attack + father.attack) / 2;
            int average_def = (mother.defense + father.defense) / 2;
            int average_spa = (mother.special_attack + father.special_attack) / 2;
            int average_spdef = (mother.special_defense + father.special_defense) / 2;
            int average_spd = (mother.speed + father.speed) / 2;

            return new Pokemon(mother.type_1, father.type_2, average_hp, average_atk, average_def, average_spa, average_spdef, average_spd);
        }

        public Pokemon MutateType(Pokemon pokemon)
        {
            Random rngesus = new Random();
            if (rngesus.Next(0, 1) == 0)
            {
                return new Pokemon(rngesus.Next(0, 17), pokemon.type_2, pokemon.hp, pokemon.attack, pokemon.defense, pokemon.special_attack, pokemon.special_defense, pokemon.speed);
            }
            return new Pokemon(pokemon.type_1, rngesus.Next(0, 17), pokemon.hp, pokemon.attack, pokemon.defense, pokemon.special_attack, pokemon.special_defense, pokemon.speed);
        }

        public Pokemon MutateStat(Pokemon pokemon)
        {
            Random rngesus = new Random();
            int switch_stat = rngesus.Next(0,5);
            int modifier = StatModifier(rngesus);

            switch (switch_stat)
            {
                case 0:
                    int hp = pokemon.hp + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, hp, pokemon.attack, pokemon.defense, pokemon.special_attack, pokemon.special_defense, pokemon.speed);
                case 1:
                    int attack = pokemon.attack + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, pokemon.hp, attack, pokemon.defense, pokemon.special_attack, pokemon.special_defense, pokemon.speed);
                case 2:
                    int defense = pokemon.defense + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, pokemon.hp, pokemon.attack, defense, pokemon.special_attack, pokemon.special_defense, pokemon.speed);
                case 3:
                    int special_attack = pokemon.special_defense + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, pokemon.hp, pokemon.attack, pokemon.defense, special_attack, pokemon.special_defense, pokemon.speed);
                case 4:
                    int special_defense = pokemon.special_defense + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, pokemon.hp, pokemon.attack, pokemon.defense, pokemon.special_attack, special_defense, pokemon.speed);
                case 5:
                    int speed = pokemon.speed + modifier;
                    return new Pokemon(pokemon.type_1, pokemon.type_2, pokemon.hp, pokemon.attack, pokemon.defense, pokemon.special_attack, pokemon.special_defense, speed);
                default:
                    return new Pokemon(pokemon);
            }
        }

        public Pokemon Reroll(Pokemon pokemon)
        {
            Random rngesus = new Random();

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
