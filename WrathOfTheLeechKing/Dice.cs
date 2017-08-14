using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Dice {

        public int Sides {
            get; set;
        }
        public int Amount {
            get; set;
        }

        public Dice(int amount, int sides) {
            this.Sides = sides;
            this.Amount = amount;
        }

        public int Roll(Random rand) {
            int result = 0;
            for (int i = 0; i < this.Amount; i++) {
                result = (rand.Next(1, this.Sides + 1));
            }
            return result;
        }

        public override string ToString() {
            return new StringBuilder().Append(Amount)
                                      .Append("d")
                                      .Append(Sides)
                                      .ToString();
        }

    }
}
