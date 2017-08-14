using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class DiceSet {

        public List<Dice> DiceList {
            get; set;
        }
        public int Modifier {
            get; set;
        }

        public DiceSet() {
            this.DiceList = new List<Dice>();
            this.Modifier = 0;
        }
        public DiceSet(Dice[] dice, int modifier) {
            this.DiceList = new List<Dice>(dice);
            this.Modifier = modifier;
        }

        public int RollDice(Random rand) {
            int result = 0;
            foreach (Dice d in this.DiceList) {
                result += d.Roll(rand);
            }
            return result + Modifier;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (Dice d in this.DiceList) {
                sb.Append(d.ToString())
                  .Append(" + ");
            }
            return sb.Append(Modifier)
                     .ToString();
        }

    }
}