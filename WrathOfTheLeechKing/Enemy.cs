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
        public string Species {
            get; set;
        }
        public string Name {
            get; set;
        }

        private void Init(int maxHP) {
            this.MaxHP = maxHP;
            this.CurrHP = maxHP;
        }
        public Enemy(int maxHp) {
            Init(maxHp);
        }
        public Enemy(EnemyData ed, Random rand, string enemyName) {

        }

        private static List<string> enemyNames = new List<string>() {
            "James", "John", "Rob", "Mike", "Will", "Davo", "Charlie", "Joseph", "Joe", "Paul", "Dan", "Mark",
            "Donald", "Eddy", "Ron", "Brian", "Tony", "Matt", "Gazza", "Timo", "Steve", "Luke", "Larry",
            "Eric", "Ray", "Andy", "Pat", "Juan", "Jack", "Frank", "Scotty",
            "Mary", "Linda", "Barb", "Liza", "Jen", "Maria", "Susy", "Lisa", "Karen", "Betty",
            "Carol", "Sandra", "Anna", "Amy", "Rebecca", "Jess", "Debra", "Joyce", "Diane", "Alice"
        };

        public struct EnemyData {
            public string species;
            public int maxHPLow, maxHPHigh;

            public EnemyData(string species, int maxHPLow, int maxHPHigh) {
                this.species = species;
                this.maxHPHigh = maxHPHigh;
                this.maxHPLow = maxHPLow;
            }
        }
    }
}
