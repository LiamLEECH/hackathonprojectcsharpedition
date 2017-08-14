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
        public List<WeaponEffects> Effects {
            get; set;
        }

        public enum WeaponEffects {
            Salty
        }

        public Weapon(string name, DiceSet damageDice, int accuracy, List<WeaponEffects> effects) {

        }

        private struct WeaponData {
            public string name;
            public DiceSet damageDice;
            public int accuracy;
            public WeaponData(string name, int accuracy, DiceSet damageDice) {
                this.name = name;
                this.damageDice = damageDice;
                this.accuracy = accuracy;
            }
        }

        private static WeaponData[] coreWeapons = {
            new WeaponData("Dagger", 75,
                            new DiceSet(new Dice[] {new Dice(1, 4)}, 0)),
            new WeaponData("Katana", 75,
                            new DiceSet(new Dice[] {new Dice(2, 6), new Dice(1, 8)}, 0)),
        };
    }
}
