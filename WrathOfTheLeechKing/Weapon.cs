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
        public Game.DamageElements WeaponElement {
            get; set;
        }

        public List<WeaponData.WeaponEffects> Effects {
            get; set;
        }

        public string EffectsToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(WeaponElement);
            foreach (WeaponData.WeaponEffects effect in Effects) {
                sb.Append(", ");
                sb.Append(effect.ToString());
            }
            return sb.ToString();
        }
        
        public Weapon(string name, DiceSet damageDice, int accuracy, int critDamagePercent, 
                      int critChance, List<WeaponData.WeaponEffects> effects, Game.DamageElements damageType) {
            this.Name = name;
            this.DamageDice = damageDice;
            this.Accuracy = accuracy;
            this.CritDamagePercent = critDamagePercent;
            this.CritChance = critChance;
            this.Effects = effects;
            this.WeaponElement = damageType;

        }

        
    }
}