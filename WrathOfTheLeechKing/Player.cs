using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Player {
        


        public int Level {
            get; set;
        }
        public int TotalExp {
            get; set;
        }
        public int MaxHP {
            get; set;
        }
        public int CurrHP {
            get; set;
        }
        public int MaxMP {
            get; set;
        }
        public int CurrMP {
            get; set;
        }
        public int Str {
            get; set;
        }
        public int Spi {
            get; set;
        }
        public int Dex {
            get; set;
        }
        public int Agi {
            get; set;
        }
        public int Con {
            get; set;
        }
        public Races Race {
            get; set;
        }

        Random growthRandom;
        

        public enum Races {
            Human, Dwarf, Elf, WolfMan, BonelessMan
        }

        public Player(Races race, Random rand) {
            RaceData rd = racesDict[race];
            Init(race, rd.maxHP, rd.maxMP, rd.str, rd.spi, rd.dex, rd.agi, rd.con, rand);
        }

        private void Init(Races race, int maxHP, int maxMP, int str, int spi, int dex, int agi, int con, Random rand) {
            this.MaxHP = maxHP;
            this.CurrHP = maxHP;
            this.MaxMP = maxMP;
            this.CurrMP = maxMP;
            this.Str = str;
            this.Spi = spi;
            this.Dex = dex;
            this.Agi = agi;
            this.Con = con;
            this.Race = race;

            this.growthRandom = rand;
            this.Level = 0;
            this.TotalExp = 0;
        }

        public static int ExpForLevel(int lv) {
            return lv * (lv + 1) * (lv + 2) / 6; // Triangles
        }

        public void LevelUp() {
            this.Level++;
            this.MaxHP += 5;
            // Todo: Implement stat ups on level up
        }

        public void CheckLevelUp() {
            if (ExpForLevel(this.Level+1) <= this.TotalExp) {
                this.LevelUp();
                this.CheckLevelUp(); // Recursion incase the player gains enough exp to level up multiple times
            }
        }


        public struct RaceData {
            public int maxHP, maxMP;
            public int str, spi, dex, agi, con;
            public RaceData(int maxHP, int maxMP, int str, int spi, int dex, int agi, int con) {
                this.maxHP = maxHP;
                this.maxMP = maxMP;
                this.str = str;
                this.spi = spi;
                this.dex = dex;
                this.agi = agi;
                this.con = con;
            }
        }

        private static Dictionary<Races, RaceData> racesDict = new Dictionary<Races, RaceData>() {
            { Races.Human, new RaceData(30, 15,
                                        3, 3, 3,    // Str Spi Dex
                                        3, 3)},     //Agi Con
            { Races.Dwarf, new RaceData(30, 12,
                                        4, 1, 3,    // Str Spi Dex
                                        2, 5)},     //Agi Con
            { Races.Elf, new RaceData(30, 20,
                                      2, 4, 3,    // Str Spi Dex
                                      3, 1)},     //Agi Con
        };

    }
}
