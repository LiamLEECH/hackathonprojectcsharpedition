using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Enemy {
        public int MaxHP {
            get; set;
        }
        public int CurrHP {
            get; set;
        }
        public DiceSet Damage {
            get; set;
        }
        public int HitChance {
            get; set;
        }
        public int DodgeChance {
            get; set;
        }
        public int PhysDefence {
            get; set;
        }
        public int MagiDefence {
            get; set;
        }
        public Game.DamageElements DamageElement {
            get; set;
        }
        public Game.DamageTypes DamageType {
            get; set;
        }
        public string Species {
            get; set;
        }
        public string Name {
            get; set;
        }

        private void Init(int maxHP, DiceSet damage, int hitChance, int dodgeChance, int physDefence, int magiDefence,
                          Game.DamageElements damageElement, Game.DamageTypes damageType, string species, string name) {
            this.MaxHP = maxHP;
            this.CurrHP = maxHP;
            this.Damage = damage;
            this.HitChance = hitChance;
            this.DodgeChance = DodgeChance;
            this.PhysDefence = physDefence;
            this.MagiDefence = magiDefence;
            this.DamageElement = damageElement;
            this.DamageType = damageType;
            this.Species = species;
            this.Name = name;
        }
        public Enemy(int maxHP, DiceSet damage, int hitChance, int dodgeChance, int physDefence, int magiDefence,
                     Game.DamageElements damageElement, Game.DamageTypes damageType, string species, string name) {
            Init(maxHP, damage, hitChance, dodgeChance, physDefence, magiDefence, damageElement, damageType, species, name);
        }
        public Enemy(EnemyData.EnemyDataStruct ed, Random rand, string enemyName) {
            Init((rand.Next(ed.maxHPLow, ed.maxHPHigh)),
                  ed.damage,
                  ed.hitChance,
                  ed.dodgeChance,
                  ed.physDefence,
                  ed.magiDefence,
                  ed.damageElement,
                  ed.damageType,
                  ed.species,
                  enemyName
                 );
        }

        public override string ToString() {
            return this.Name + " the " + this.Species;
        }

        
    }
}
