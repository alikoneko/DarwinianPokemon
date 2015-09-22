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
        private int type_1;
        //private int type_2;
        private int hp;
        private int attack;
        private int defense;
        //private int special_attack;
        //private int special_defense;
        //private int speed;
        //private string name;

        public Pokemon(int type_1, int hp, int attack, int defense)
        {
            this.type_1 = type_1;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            //this.name = name;
        }
        //public Pokemon(int type_1, int type_2, int hp, int attack, int defense, int special_attack, int special_defense, int speed, string name)
        //{
        //    this.type_1 = type_1;
        //    this.type_2 = type_2;
        //    this.hp = hp;
        //    this.attack = attack;
        //    this.defense = defense;
        //    this.special_attack = special_attack;
        //    this.special_defense = special_defense;
        //    this.speed = speed;
        //    this.name = name;
        //}

        public Pokemon(Pokemon pokemon)
        {
            this.type_1 = pokemon.type_1;
            //this.type_2 = pokemon.type_2;
            this.hp = pokemon.hp;
            this.attack = pokemon.attack;
            this.defense = pokemon.defense;
            //this.special_attack = pokemon.special_attack;
            //this.special_defense = pokemon.special_defense;
            //this.speed = pokemon.speed;
            //this.name = pokemon.name;
        }

        public override string ToString()
        {
            string pokemon = "";
            //pokemon += name + "\n";
            pokemon += type_1 + "\n";
            pokemon += "Stats:\n";
            pokemon += "hp: " + hp + "\n";
            pokemon += "atk: " + attack + "\n";
            pokemon += "defense: " + defense + "\n";
            return pokemon;
        }
    }
}
