using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarwinianPokemon.Attacks;

namespace DarwinianPokemon
{
    //enum Types { Normal, Fire, Fighting, Water, Grass, }
    class Pokemon
    {
        private int type_1;
        private int type_2;
        private int hp;
        private int attack;
        private int defense;
        private int special_attack;
        private int special_defense;
        private int speed;
        private int damage = 0;
        private Random random;
        private List<Attack> attacks;
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
            this.attacks = new List<Attack> { new Surf() };
            random = ServiceRegistry.GetInstance().GetRandom();
            //this.name = name;
        }

        public Pokemon(Pokemon pokemon)
        {
            type_1 = pokemon.type_1;
            type_2 = pokemon.type_2;
            hp = pokemon.hp;
            attack = pokemon.attack;
            defense = pokemon.defense;
            special_attack = pokemon.special_attack;
            special_defense = pokemon.special_defense;
            speed = pokemon.speed;
            this.attacks = new List<Attack> { new Surf() };
            random = ServiceRegistry.GetInstance().GetRandom();
            //this.name = pokemon.name;
        }

        public void Damage(int damage)
        {
            this.damage += damage;
        }
        public override string ToString()
        {
            string pokemon = "";
            //pokemon += name + "\n";
            pokemon += type_1 + " " + type_2 + "\n"
                + "Stats:\n"
                + "max hp: " + hp + "\n"
                + "hp: " + GetHP() + "\n"
                + "atk: " + attack + "\n"
                + "defense: " + defense + "\n"
                + "special attack: " + special_attack + "\n"
                + "special defense: " + special_defense + "\n"
                + "speed: " + speed + "\n";
            return pokemon;
        }

        public void Attack(Pokemon target)
        {
            target.Damage(attacks[random.Next(attacks.Count)].GetDamage(this, target));
        }

        public bool Dead()
        {
            return GetHP() < 0;
        }

        public int Level()
        {
            return 50;
        }

        public void Heal()
        {
            damage = 0;
        }

        public int GetHP()
        {
            return hp - damage;
        }

        public int GetMaxHP()
        {
            return hp;
        }

        public int GetAttack()
        {
            return attack;
        }

        public int GetDefense() 
        {
            return this.defense;
        }

        public int GetSpecialAttack()
        {
            return this.special_attack;
        }

        public int GetSpecialDefense()
        {
            return this.special_defense;
        }

        public int GetSpeed()
        {
            return this.speed;
        }

        public int GetType_1()
        {
            return this.type_1;
        }

        public int GetType_2()
        {
            return this.type_2;
        }
    }
}
