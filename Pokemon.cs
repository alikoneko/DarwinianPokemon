﻿using System;
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
        private static StatRange HP_RANGE = new StatRange(50, 250);
        private static StatRange ATTACK_RANGE = new StatRange(25, 150);
        private static StatRange DEFENSE_RANGE = new StatRange(50, 200);
        private static StatRange SPECIAL_ATTACK_RANGE = new StatRange(25, 150);
        private static StatRange SPECIAL_DEFENSE_RANGE = new StatRange(50, 200);
        private static StatRange SPEED_RANGE = new StatRange(50, 100);

        const int MUTATE_STAT_AMOUNT = 10;

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

            Pokemon baby = new Pokemon(type_1, father.GetType_2(), average_hp, average_atk, average_def, average_spa, average_spdef, average_spd, NameGenerator.Generate(name, father.name));
            int mutate = random.Next(0, 7);
            switch (mutate)
            {
                case 0:
                    baby.MutateType();
                    break;
                case 1:
                    baby.MutateStat();
                    break;
                case 2:
                    baby.Reroll();
                    break;
                default:
                    break;
            }

            return baby;

        }

        private void MutateType()
        {
            if (random.FlipCoin())
            {
                type_1 = RandomType();
            }
            else
            {
                type_2 = RandomType();
            }
        }

        private int RandomType()
        {
            return random.Next(0, 19);
        }

        private void MutateStat()
        {
            int switch_stat = random.Next(0, 6);
            int modifier = random.Next(-MUTATE_STAT_AMOUNT, MUTATE_STAT_AMOUNT+1);

            switch (switch_stat)
            {
                case 0:
                    hp = HP_RANGE.Clamp(hp + modifier);
                    break;
                case 1:
                    attack = ATTACK_RANGE.Clamp(attack + modifier);
                    break;
                case 2:
                    defense = DEFENSE_RANGE.Clamp(defense + modifier);
                    break;
                case 3:
                    special_attack = SPECIAL_ATTACK_RANGE.Clamp(special_attack + modifier);
                    break;
                case 4:
                    special_defense = SPECIAL_DEFENSE_RANGE.Clamp(special_defense + modifier);
                    break;
                case 5:
                    speed = SPEED_RANGE.Clamp(speed + modifier);
                    break;
                default:
                    break;
            }
        }

        private void Reroll()
        {
            type_1 = RandomType();
            type_2 = RandomType();
            hp = random.Next(20, hp + 15);
            attack = random.Next(20, attack + 15);
            defense = random.Next(20, defense + 15);
            special_attack = random.Next(20, special_attack + 15);
            special_defense = random.Next(20, special_defense + 15);
            speed = random.Next(20, speed + 15);
        }

        public override string ToString()
        {
            string pokemon = "";
            pokemon += name + "\n";
            pokemon += type_1 + " " + type_2 + "\n"
                + "Age: " + age + "\n"
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

        public bool Breedable
        {
            get
            {
                return age >= max_age * 0.2;
            }
        }
        public int Age
        {
            get
            {
                return age;
            }
        }
        public bool Dead
        {
            get
            {
                return GetHP() <= 0 || age >= max_age;
            }
        }

        public void SetInitialAge()
        {
            age = (int)(max_age * 0.2);
        }

        public void Attack(Pokemon target)
        {
            target.Damage(attacks[random.Next(attacks.Count)].GetDamage(this, target));
        }

        public int Level()
        {
            return 50;
        }

        public void Heal()
        {
            damage -= (int)(GetMaxHP() * 0.20);
            if (damage < 0)
            {
                damage = 0;
            }
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

        public string GetName()
        {
            return name;
        }
    }
}
