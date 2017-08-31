using System;

namespace WrathOfTheLeechKing {
    class Charm {

        // Each Charm is just a some dice atm, but may have more stuff later.
        // You can use a single charm when your attack hits in order to deal more damage.

        public int Sides {
            get; set;
        }
        public int NDice {
            get; set;
        }
        public int AmountOwned {
            get; set;
        }
        public override string ToString() {
            return this.NDice + "d" + this.Sides;
        }
        public int Roll(Random rand) {
            return new Dice(this.NDice, this.Sides).Roll(rand);
        }


    }
}