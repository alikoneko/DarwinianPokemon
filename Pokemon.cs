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
            max_age = DetermineLifeSpan();
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
            if (random.Next(0, 1) == 0)
            {
                return new Pokemon(random.Next(0, 17), pokemon.GetType_2(), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(),
                    pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
            }
            return new Pokemon(pokemon.GetType_1(), random.Next(0, 17), pokemon.GetHP(), pokemon.GetAttack(), pokemon.GetDefense(),
                pokemon.GetSpecialAttack(), pokemon.GetSpecialDefense(), pokemon.GetSpeed());
        }

        public Pokemon MutateStat(Pokemon pokemon)
        {
            int switch_stat = random.Next(0, 5);
            int modifier = StatModifier(random);

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
            return new Pokemon(random.Next(0, 17), random.Next(0, 17), random.Next(pokemon.GetHP() - 30, pokemon.GetHP() + 30), random.Next(pokemon.GetAttack() - 30, pokemon.GetAttack() + 30),
                random.Next(pokemon.GetDefense() - 30, pokemon.GetDefense() + 30), random.Next(pokemon.GetSpecialAttack() - 30, pokemon.GetSpecialAttack() + 30),
                random.Next(pokemon.GetSpecialDefense() - 30, pokemon.GetSpecialDefense() + 30), random.Next(pokemon.GetSpeed() - 30, pokemon.GetSpeed() + 30));
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

        private int DetermineLifeSpan()
        {
            return (hp + attack + defense + special_attack + special_defense + speed) / 5;
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
