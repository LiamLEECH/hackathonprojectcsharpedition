using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Enemy {

        public enum AttackTypes {
            Phys, Magi
        }

        public int MaxHP {
            get; set;
        }
        public int CurrHp {
            get; set;
        }
        public int DamageDice {
            get; set;
        }
        public int SidesDice {
            get; set;
        }
        public int DamageModifier {
            get; set;
        }
        public int DodgeChance {
            get; set;
        }
        public int HitChance {
            get; set;
        }
        public int Defence {
            get; set;
        }
        public AttackTypes AttackType {
            get; set;
        }
        public string EnemyTitle {
            get; set;
        }
        public string EnemyName {
            get; set;
        }
        private void Init(int maxHP, int damageDice, int sidesDice, int damageModifier, int dodgeChance, int hitChance, int defence, AttackTypes attackType, string enemyTitle, string enemyName) {
            MaxHP = maxHP;
            CurrHp = maxHP;
            DamageDice = damageDice;
            SidesDice = sidesDice;
            DamageModifier = damageModifier;
            DodgeChance = dodgeChance;
            HitChance = hitChance;
            Defence = defence;
            AttackType = attackType;
            EnemyTitle = enemyTitle;
            EnemyName = enemyName;
        }
        public Enemy(int maxHP, int damageDice, int sidesDice, int damageModifier, int dodgeChance, int hitChance, int defence, AttackTypes attackType, string enemyTitle, string enemyName) {
            Init(
                maxHP, damageDice, sidesDice, damageModifier, dodgeChance, hitChance, defence, attackType, enemyTitle, enemyName
            );
        }
        public Enemy(EnemyData ed, Random rand, string enemyName) {
            Init(
                (rand.Next(ed.maxHPLow, ed.maxHPHigh)),
                ed.damageDice,
                ed.sidesDice,
                ed.damageMod,
                ed.dodgeChance,
                ed.hitChance,
                ed.defence,
                ed.attackType,
                ed.title,
                enemyName
            );
        }

        public override string ToString() {
            return EnemyName + " the " + EnemyTitle;
        }

        public bool DealDamage(int dmg) {
            this.CurrHp -= dmg;
            if (CurrHp <= 0) return true;
            return false;
        }

        /*
         * Static enemy data
         */
        public static List<Enemy> GenerateEnemyParty (int difficulty, Random rand) {
            List<Enemy> e = new List<Enemy>();
            HelperClass.ShuffleList<string>(enemyNames);
            Queue<string> usableNames = new Queue<string>(enemyNames);

            switch (difficulty) {
                case 1:
                    switch (rand.Next(5)) {
                        case 0: // Two Slimes
                            e.Add(new Enemy(enemies["Goo"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Goo"], rand, usableNames.Dequeue()));
                            break;
                        case 1: // Two Ghosts
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            break;
                        case 2: // Three Rats
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            break;
                        case 3: // One Knight
                            e.Add(new Enemy(enemies["Knight"], rand, usableNames.Dequeue()));
                            break;
                        case 4: // One rat + either ghost or slime
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            if (rand.Next(1)==0) {
                                e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            } else {
                                e.Add(new Enemy(enemies["Goo"], rand, usableNames.Dequeue()));
                            }
                            break;
                    }
                    break;
                case 2:
                    switch (rand.Next(5)) {
                        case 0: // Slime + Dog or Cat
                            e.Add(new Enemy(enemies["Goo"], rand, usableNames.Dequeue()));
                            if (rand.Next(1)==0) {
                                e.Add(new Enemy(enemies["Dog"], rand, usableNames.Dequeue()));
                            } else {
                                e.Add(new Enemy(enemies["Cat"], rand, usableNames.Dequeue()));
                            }
                            break;
                        case 1: // Ghost + Cat
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Cat"], rand, usableNames.Dequeue()));
                            break;
                        case 2: // Dog + Knight
                            e.Add(new Enemy(enemies["Dog"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Knight"], rand, usableNames.Dequeue()));
                            break;
                        case 3: // Three Ghosts
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                            break;
                        case 4: // Two Rat + either Knight or Goo
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            e.Add(new Enemy(enemies["Rat"], rand, usableNames.Dequeue()));
                            if (rand.Next(1) == 0) {
                                e.Add(new Enemy(enemies["Knight"], rand, usableNames.Dequeue()));
                            } else {
                                e.Add(new Enemy(enemies["Goo"], rand, usableNames.Dequeue()));
                            }
                            break;
                    }
                    break;
                default:
                    if (rand.Next(1) == 0) {
                        e.Add(new Enemy(enemies["Knight"], rand, usableNames.Dequeue()));
                        e.Add(new Enemy(enemies["Knight"], rand, usableNames.Dequeue()));
                    } else {
                        e.Add(new Enemy(enemies["Cat"], rand, usableNames.Dequeue()));
                        e.Add(new Enemy(enemies["Dog"], rand, usableNames.Dequeue()));
                        e.Add(new Enemy(enemies["Ghost"], rand, usableNames.Dequeue()));
                    }
                    break;
            }
            return e;
        }
        private static List<string> enemyNames = new List<string>() {
            "James", "John", "Rob", "Mike", "Will", "Davo", "Charlie", "Joseph", "Joe", "Paul", "Dan", "Mark",
            "Donald", "Eddy", "Ron", "Brian", "Tony", "Matt", "Gazza", "Timo", "Steve",
            "Eric", "Ray", "Andy", "Pat", "Juan", "Jack", "Frank", "Scotty",
            "Mary", "Linda", "Barb", "Liza", "Jen", "Maria", "Susy", "Lisa", "Karen", "Betty",
            "Carol", "Sandra", "Anna", "Amy", "Rebecca", "Jess", "Debra", "Joyce", "Diane", "Alice"
        };
        public struct EnemyData {
            public string title;
            public int maxHPLow;
            public int maxHPHigh;
            public int damageDice;
            public int sidesDice;
            public int damageMod;
            public int hitChance;
            public int dodgeChance;
            public int defence;
            public AttackTypes attackType;
            public EnemyData(string name, int maxHPLow, int maxHPHigh, int damageDice, int sidesDice, int damageMod, int hitChance, int dodgeChance, int defence, AttackTypes attackType) {
                this.title = name;
                this.maxHPLow = maxHPLow;
                this.maxHPHigh = maxHPHigh;
                this.damageDice = damageDice;
                this.sidesDice = sidesDice;
                this.damageMod = damageMod;
                this.hitChance = hitChance;
                this.dodgeChance = dodgeChance;
                this.defence = defence;
                this.attackType = attackType;
            }
        }

        private static Dictionary<string, EnemyData> enemies = new Dictionary<string, EnemyData>() {
            { "Goo", new EnemyData("Puddle of Goo", // Name
                                   22, 25, // Low and High HP
                                   2, 4, 1, // Dice, Sides, Mod
                                   75, 10, // Hit%, Dodge%
                                   2, AttackTypes.Phys)}, // Def, Atype
            { "Rat", new EnemyData("Giant Rat", // Name
                                   15, 19, // Low and High HP
                                   1, 6, 1, // Dice, Sides, Mod
                                   75, 14, // Hit%, Dodge%
                                   1, AttackTypes.Phys)}, // Def, Atype
            { "Ghost", new EnemyData("Spoopy Ghost", // Name
                                   20, 24, // Low and High HP
                                   1, 8, 0, // Dice, Sides, Mod
                                   80, 12, // Hit%, Dodge%
                                   1, AttackTypes.Magi)}, // Def, Atype
            { "Knight", new EnemyData("Lost Knight", // Name
                                   31, 36, // Low and High HP
                                   2, 6, 2, // Dice, Sides, Mod
                                   75, 8, // Hit%, Dodge%
                                   3, AttackTypes.Phys)}, // Def, Atype
            { "Dog", new EnemyData("Dog", // Name
                                   30, 34, // Low and High HP
                                   1, 12, 3, // Dice, Sides, Mod
                                   80, 11, // Hit%, Dodge%
                                   2, AttackTypes.Phys)}, // Def, Atype
            { "Cat", new EnemyData("Wizarding Cat", // Name
                                   26, 30, // Low and High HP
                                   2, 4, 4, // Dice, Sides, Mod
                                   80, 12, // Hit%, Dodge%
                                   1, AttackTypes.Magi)}, // Def, Atype
        };
        


    }
}
