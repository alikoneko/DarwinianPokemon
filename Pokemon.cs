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
        private int age = 0;
        private int max_age;
        private Random random;
        private List<IAttack> attacks;
        private string name;

        public Pokemon(int type_1, int type_2, int hp, int attack, int defense, int special_attack, int special_defense, int speed, string name)
        {
            this.type_1 = type_1;
            this.type_2 = type_2;
            this.hp = hp;
            this.attack = attack;
            this.defense = defense;
            this.special_attack = special_attack;
            this.special_defense = special_defense;
            this.speed = speed;
            Initialize();
            this.name = name;
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
            Initialize();
            this.name = pokemon.name;
        }

        private void Initialize()
        {
            random = ServiceRegistry.GetInstance().GetRandom();
            this.attacks = new List<IAttack> 
            { 
                new Surf(),
                new HyperVoice(),
                new Pound(),
                new Gust()
            };
            max_age = (hp + attack + defense + special_attack + special_defense + speed) / 5;
        }
        public void Damage(int damage)
        {
            this.damage += damage;
        }

        public Pokemon Breed(Pokemon father)
        {
            int average_hp = (GetMaxHP() + father.GetHP()) / 2;
            int average_atk = (attack + father.GetAttack()) / 2;
            int average_def = (defense + father.GetDefense()) / 2;
            int average_spa = (special_attack + father.GetSpecialAttack()) / 2;
            int average_spdef = (special_defense + father.GetSpecialDefense()) / 2;
            int average_spd = (speed + father.GetSpeed()) / 2;

            return new Pokemon(type_1, father.GetType_2(), average_hp, average_atk, average_def, average_spa, average_spdef, average_spd, NameGenerator.Generate(name, father.name));
        }

        public Pokemon MutateType()
        {
            if (random.Next(0, 1) == 0)
            {
                return new Pokemon(random.Next(0, 17), type_2, hp, attack, defense, special_attack, special_defense, speed, name);
            }
            return new Pokemon(type_1, random.Next(0, 17), hp, attack, defense, special_attack, special_defense, speed, name);
        }

        public Pokemon MutateStat()
        {
            int switch_stat = random.Next(0, 5);
            int modifier = StatModifier();

            switch (switch_stat)
            {
                case 0:
                    int hp = this.hp + modifier;
                    return new Pokemon(type_1, type_2, hp, this.attack, this.defense, this.special_attack, this.special_defense, this.speed, name);
                case 1:
                    int attack = this.attack + modifier;
                    return new Pokemon(type_1, type_2, this.hp, attack, this.defense, this.special_attack, this.special_defense, this.speed, name);
                case 2:
                    int defense = this.defense + modifier;
                    return new Pokemon(type_1, type_2, this.hp, this.attack, defense, this.special_attack, this.special_defense, this.speed, name);
                case 3:
                    int special_attack = this.special_defense + modifier;
                    return new Pokemon(type_1, type_2, this.hp, this.attack, this.defense, special_attack, this.special_defense, this.speed, name);
                case 4:
                    int special_defense = this.special_defense + modifier;
                    return new Pokemon(type_1, type_2, this.hp, this.attack, this.defense, this.special_attack, special_defense, this.speed, name);
                case 5:
                    int speed = this.speed + modifier;
                    return new Pokemon(type_1, type_2, this.hp, this.attack, this.defense, this.special_attack, this.special_defense, speed, name);
                default:
                    return new Pokemon(this);
            }
        }

        public Pokemon Reroll()
        {
            return new Pokemon(random.Next(0, 17), random.Next(0, 17), random.Next(hp - 30, hp + 30), random.Next(attack - 30, attack + 30),
                random.Next(defense - 30, defense + 30), random.Next(special_attack - 30, special_attack + 30),
                random.Next(special_defense - 30, special_defense + 30), random.Next(speed - 30, speed + 30), name);
        }

        private int StatModifier()
        {
            int modifier = random.Next(0, 1);

            if (modifier == 0)
            {
                modifier = -1;
            }

            return random.Next(0, 10) * modifier;

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
            if (age >= max_age)
            {
                return age >= max_age; 
            }

            return GetHP() < 0;
        }

        public int Level()
        {
            return 50;
        }

        public void Heal()
        {
            damage -= (int)(damage * 0.20);
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

        public void IncreaseAge()
        {
            age++;
        }

        public void max_heal()
        {
            damage = 0;
        }
    }
}
