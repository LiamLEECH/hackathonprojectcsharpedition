using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class EnemyData {


        public static List<Enemy> GenerateEnemyParty(Random rand, int diff) {
            List<Enemy> e = new List<Enemy>();
            Queue<string> names = GetShuffledNames(rand);
            switch (diff) {
                case 1:
                    switch (rand.Next(5)) {
                        case 0:
                            e.Add(new Enemy(enemiesDict["Goo"], rand, names.Dequeue()));
                            e.Add(new Enemy(enemiesDict["Goo"], rand, names.Dequeue()));
                            break;
                        case 1:
                            e.Add(new Enemy(enemiesDict["Ghost"], rand, names.Dequeue()));
                            e.Add(new Enemy(enemiesDict["Ghost"], rand, names.Dequeue()));
                            break;
                        case 2:
                            e.Add(new Enemy(enemiesDict["Rat"], rand, names.Dequeue()));
                            e.Add(new Enemy(enemiesDict["Rat"], rand, names.Dequeue()));
                            e.Add(new Enemy(enemiesDict["Rat"], rand, names.Dequeue()));
                            break;
                        case 3:
                            e.Add(new Enemy(enemiesDict["Spider"], rand, names.Dequeue()));
                            if (rand.Next(2) == 0) {
                                e.Add(new Enemy(enemiesDict["Goo"], rand, names.Dequeue()));
                            } else {
                                e.Add(new Enemy(enemiesDict["Rat"], rand, names.Dequeue()));
                            }
                            break;
                        case 4:
                            e.Add(new Enemy(enemiesDict["Eye"], rand, names.Dequeue()));
                            if (rand.Next(2) == 0) {
                                e.Add(new Enemy(enemiesDict["Eye"], rand, names.Dequeue()));
                            } else {
                                e.Add(new Enemy(enemiesDict["Ghost"], rand, names.Dequeue()));
                            }
                            break;
                    }
                    break;
                case 2:
                    break;
            }
            return e;
        }

        public static void PrintNames(Random rand) {
            Queue<string> n = GetShuffledNames(rand);
            foreach (string s in n) {
                Console.Write(s + ", ");
            }
        }


        private static List<string> enemyNames = new List<string>() {
            "James", "John", "Rob", "Mike", "Will", "Davo", "Charlie", "Joseph", "Joe", "Paul", "Dan", "Mark",
            "Donald", "Eddy", "Ron", "Brian", "Tony", "Matt", "Gazza", "Timo", "Steve", "Luke", "Larry",
            "Eric", "Ray", "Andy", "Pat", "Juan", "Jack", "Frank", "Scotty",
            "Mary", "Linda", "Barb", "Liza", "Jen", "Maria", "Susy", "Lisa", "Karen", "Betty",
            "Carol", "Sandra", "Anna", "Amy", "Rebecca", "Jess", "Debra", "Joyce", "Diane", "Alice"
        };

        public static Queue<string> GetShuffledNames(Random rand) {
            int n = enemyNames.Count;
            while (n > 1) {
                n--;
                int k = rand.Next(n + 1);
                string value = enemyNames[k];
                enemyNames[k] = enemyNames[n];
                enemyNames[n] = value;
            }
            return new Queue<string>(enemyNames);
        }

        public struct EnemyDataStruct {
            public int maxHPLow, maxHPHigh;
            public DiceSet damage;
            public int hitChance;
            public int dodgeChance;
            public int physDefence;
            public int magiDefence;
            public Game.DamageTypes damageType;
            public Game.DamageElements damageElement;
            public string species;

            public EnemyDataStruct(string species, int maxHPLow, int maxHPHigh, DiceSet damage, int physDefence, int magiDefence, int hitChance, int dodgeChance, Game.DamageElements damageElement, Game.DamageTypes damageType) {
                this.maxHPLow = maxHPLow;
                this.maxHPHigh = maxHPHigh;
                this.damage = damage;
                this.hitChance = hitChance;
                this.dodgeChance = dodgeChance;
                this.physDefence = physDefence;
                this.magiDefence = magiDefence;
                this.damageElement = damageElement;
                this.damageType = damageType;
                this.species = species;
            }
        }

        private static Dictionary<string, EnemyDataStruct> enemiesDict = new Dictionary<string, EnemyDataStruct>() {
            {
                "Goo", new EnemyDataStruct("Puddle of Goo",             // Species Name
                                           23, 25,                      // MaxHp low-high
                                           new DiceSet(),               // Damage Dice
                                           5, 5,                        // Phys and Mag Defence
                                           5, 5,                        // Hit, Dodge      
                                           Game.DamageElements.None,    // Damage Element
                                           Game.DamageTypes.Phys)       // Damage Type
            }, {
                "Rat", new EnemyDataStruct("Giant Rat",                 // Species Name
                                           23, 25,                      // MaxHp low-high
                                           new DiceSet(),               // Damage Dice
                                           5, 5,                        // Phys and Mag Defence
                                           5, 5,                        // Hit, Dodge      
                                           Game.DamageElements.None,    // Damage Element
                                           Game.DamageTypes.Phys)       // Damage Type
            }, {
                "Spider", new EnemyDataStruct("Deadly Spider",          // Species Name
                                           23, 25,                      // MaxHp low-high
                                           new DiceSet(),               // Damage Dice
                                           5, 5,                        // Phys and Mag Defence
                                           5, 5,                        // Hit, Dodge      
                                           Game.DamageElements.None,    // Damage Element
                                           Game.DamageTypes.Phys)       // Damage Type
            }, {
                "Ghost", new EnemyDataStruct("Spoopy Ghost",            // Species Name
                                           23, 25,                      // MaxHp low-high
                                           new DiceSet(),               // Damage Dice
                                           5, 5,                        // Phys and Mag Defence
                                           5, 5,                        // Hit, Dodge      
                                           Game.DamageElements.None,    // Damage Element
                                           Game.DamageTypes.Phys)       // Damage Type
            }, {
                "Eye", new EnemyDataStruct("Floating Eye",              // Species Name
                                           23, 25,                      // MaxHp low-high
                                           new DiceSet(),               // Damage Dice
                                           5, 5,                        // Phys and Mag Defence
                                           5, 5,                        // Hit, Dodge      
                                           Game.DamageElements.None,    // Damage Element
                                           Game.DamageTypes.Phys)       // Damage Type
            }
        };
    }
}
