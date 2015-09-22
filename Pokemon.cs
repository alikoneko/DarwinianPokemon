using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarwinianPokemon
{
    //enum Types { Normal, Fire, Fighting, Water, Grass, }
    class Pokemon
    {
        public int type_1 { get; set; }
        public int type_2 { get; set; }
        public  int hp { get; set; }
        public int attack { get; set; }
        public int defense { get; set; }
        public int special_attack { get; set; }
        public int special_defense { get; set; }
        public int speed { get; set; }
        //private string name;

        public Pokemon(int type_1, int type_2, int hp, int attack, int defense, int special_attack, int special_defense, int speed)
        {
            this.type_1 = type_1;
            this.type_2 = type_2;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.special_attack = special_attack;
            this.special_defense = special_defense;
            this.speed = speed;
            //this.name = name;
        }

        public Pokemon(Pokemon pokemon)
        {
            this.type_1 = pokemon.type_1;
            this.type_2 = pokemon.type_2;
            this.hp = pokemon.hp;
            this.attack = pokemon.attack;
            this.defense = pokemon.defense;
            this.special_attack = pokemon.special_attack;
            this.special_defense = pokemon.special_defense;
            this.speed = pokemon.speed;
            //this.name = pokemon.name;
        }

        public override string ToString()
        {
            string pokemon = "";
            //pokemon += name + "\n";
            pokemon += type_1 + " " + type_2 + "\n"
                + "Stats:\n"
                + "hp: " + hp + "\n"
                + "atk: " + attack + "\n"
                + "defense: " + defense + "\n"
                + "special attack: " + special_attack + "\n"
                + "special defense: " + special_defense + "\n"
                + "speed: " + speed + "\n";
            return pokemon;
        }
    }
}
