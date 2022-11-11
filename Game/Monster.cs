using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    internal class Monster
    {

        // name, health, damage, loot - based on random generated level

        private string _name;
        private int _health;
        private int _hitChance;
        private int _block;
        private int _damage;

        public Monster()
            {
            }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public int Health
        {
            get { return this._health; }
            set { this._health = value; }
        }

        public int HitChance
        {
            get { return this._hitChance; }
            set { this._hitChance = value; }
        }

        public int Block
        {
            get { return this._block; }
            set { this._block = value; }
        }

        public int Damage
        {
            get { return this._damage; }
            set { this._damage = value; }
        }



    }
}
