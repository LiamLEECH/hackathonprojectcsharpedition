using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Player {

        public int MaxHP {
            get; set;
        }
        public int CurrHP {
            get; set;
        }
        public int Level {
            get; set;
        }
        private int totalExp;
        public int TotalExp {
            get {
                return totalExp;
            }
            set {
                totalExp = value;
                CheckLevelUp(HelperClass.rand);
            }
        }
        public int Str {
            get; set;
        }
        public int Dex {
            get; set;
        }
        public int Agi {
            get; set;
        }
        public int Def {
            get {
                return 0;
            }
        }
        public int MDef {
            get {
                return 0;
            }
        }
        public Weapon eWeapon {
            get; set;
        }
        public Player(Random rand) {
            MaxHP = 25; CurrHP = 25;
            Level = 1;
            TotalExp = 0;
            Str = 0;
            Dex = 0;
            Agi = 0;
            eWeapon = Weapon.GenerateWeapon(0, rand);
            int i = 0;
            while (i < 3) {
                switch (rand.Next(0, 2)) { // Get 3 random stat ups, but each stat cant go past 2 here
                    case 0: // str
                        if (Str < 2) {
                            Str++;
                            i++; 
                        }
                        break;
                    case 1: // dex
                        if (Dex < 2) {
                            Dex++;
                            i++;
                        }
                        break;
                    case 2:
                        if (Agi < 2) {
                            Agi++;
                            i++;
                        }
                        break;
                }
            }
        }

        private void LevelUp(Random rand) {
            Level++;
            // Randomly increase Str, Dex, or Agi, and increase HP by a small amount.
            switch (rand.Next(0, 2)) { // Get 3 random stat ups, but each stat cant go past 2 here
                case 0: // str
                    Str++;
                    break;
                case 1: // dex
                    Dex++;
                    break;
                case 2: // Agi
                    Agi++;
                    break;
            }
            MaxHP += (rand.Next(2, 4));
        }

        public void CheckLevelUp(Random rand) {
            if (ExpForLevel(this.Level+1) <= this.TotalExp) {
                this.LevelUp(rand);
                this.CheckLevelUp(rand);
            }
        }

        public static int ExpForLevel(int lv) {
            return lv * (lv + 1) * (lv + 2) / 6;
        }
    }
}
