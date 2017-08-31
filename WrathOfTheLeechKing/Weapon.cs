using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Weapon {
        public string Name {
            get; set;
        }
        public DiceSet DamageDice {
            get; set;
        }
        public int Accuracy {
            get; set;
        }
        public int CritDamagePercent {
            get; set;
        }
        public int CritChance {
            get; set;
        }
        public List<Game.DamageElements> WeaponElements {
            get; set;
        }

        public List<WeaponData.WeaponEffects> Effects {
            get; set;
        }

        public string EffectsToString() {
            StringBuilder sb = new StringBuilder();
            foreach (Game.DamageElements elem in WeaponElements) {
                sb.Append(" " + elem);
                sb.Append(", ");
            }
            for (int i  = 0; i < this.Effects.Count; i++) {
                if (i != 0) sb.Append(", ");
                sb.Append(Effects[i].ToString());
            }
            return sb.ToString();
        }

        public override string ToString() {
            return new StringBuilder().Append(this.Name)
                                   .Append(": Damage Dice: ").Append(this.DamageDice.ToString())
                                   .Append(", Acc: ").Append(this.Accuracy)
                                   .Append(", Crit Chance: ").Append(this.CritChance)
                                   .Append(", Crit Multiplier: ").Append((float)this.CritDamagePercent / 100.0f)
                                   .ToString();
        }

        public Weapon(string name, DiceSet damageDice, int accuracy, int critDamagePercent, 
                      int critChance, List<WeaponData.WeaponEffects> effects, List<Game.DamageElements> damageTypes) {
            this.Name = name;
            this.DamageDice = damageDice;
            this.Accuracy = accuracy;
            this.CritDamagePercent = critDamagePercent;
            this.CritChance = critChance;
            this.Effects = effects;
            this.WeaponElements = damageTypes;

        }

        
    }
}