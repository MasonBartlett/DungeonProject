using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{

/*
    public class simpleWeapon
    {
        private string simpleName;
        private int simplePower;
        private int simpleCrit;

        public simpleWeapon(string simpleName, int simplePower, int simpleCrit)
        {
            this.simpleName = simpleName;
            this.simplePower = simplePower;
            this.simpleCrit = simpleCrit;
        }

    }
/*/
    public class Weapon
    {

        private string _name;
        private int _minDamage;
        private int _maxDamage;
        private bool _isTwoHanded;
        private int _bonusHitChance;


        

        public int MaxDamage
        {
            get {return _maxDamage;}
            set {_maxDamage = value;}
        }

        public int MinDamage
        {
            get { return _minDamage; }
            set { _minDamage = value; }

            /*
            set
            {
                if (value > 0 && value <= MaxDamage)
                {
                    _minDamage = value;
                }
                else 
                { 
                    
                }
            }
            */
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }

           
        }

        public bool IsTwoHanded
        {
            get { return _isTwoHanded; }
            set { _isTwoHanded = value; }
        }

        public int BonusHitChance
        { 
            get { return _bonusHitChance; }
            set { _bonusHitChance = value; }
        }


        public Weapon(string _name, int _minDamage, int _maxDamage, bool _isTwoHanded, int _bonusHitChance)
        {
            this._name = _name;
            this._minDamage = _minDamage;
            this._maxDamage = _maxDamage;
            this._isTwoHanded = _isTwoHanded;
            this._bonusHitChance = _bonusHitChance;
        }

        public Weapon()
        { 
        }

    }



   
}
