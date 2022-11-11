using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    
    internal class Character
    {
        //name, hit chance, max life, health, block, equipped weapon

        private string _name;
        private int _hitChance;
        private int _health;
        private int _block;
        private int _maxHealth;
        private Weapon _weapon;
        private string _hasPerk;

        public Character(string _name, int _hitChance, int _health, int _block, Weapon _weapon)
        {
            this._name = _name;
            this._hitChance = _hitChance;
            this._health = _health;
            this._block = _block;
            this._weapon = _weapon;
        }

        public Character()
        {
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        public int MaxHealth
        {
            get { return this._maxHealth; }
            set { this._maxHealth = value; }
        }

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value <= MaxHealth)
                {
                    _health = value;
                }
                else
                {
                    _health = _maxHealth;
                }
            }
        }

        public Weapon charWeapon
        {
            get { return this._weapon; }
            set { this._weapon = value; }
        }


        

        public int HitChance
        {
            get { return this._hitChance; }
            set { this._hitChance = value; }
        }


        public string HasPerk
        {
            get { return this._hasPerk; } 
            set { this._hasPerk = value; }
        }

        public int Block
        {
            get { return this._block; }
            set { this._block = value; }
        }
            

        /* 
            Ability to create a character object to be used in the dungeon for creating your player and the monsters they battle
            Calculate the hit chance (e.g. player hit chance + weapon bonus hit chance)
            Calculate the damage (e.g. using System.Random to choose a number between the equipped weapon minimum and maximum damage)
         */




    }
}
