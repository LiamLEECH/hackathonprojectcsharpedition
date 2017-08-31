using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class DiceSet {

        private List<Dice> diceList;
        public List<Dice> DiceList {
            get {
                return diceList;
            } set {
                diceList = value;
            }
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

        public void AddDice(Dice d) {
            int dindex = diceList.FindIndex((x) => x.Sides == d.Sides);
            if (dindex < 0) {
                diceList.Add(d);
            } else {
                diceList[dindex].Amount += d.Amount;
            }
        }
        public int RollDice(Random rand) {
            int result = 0;
            foreach (Dice d in this.DiceList) {
                result += d.Roll(rand);
            }
            return result + Modifier;
        }
        private void OrderDice() {
            diceList.Sort((a, b) => a.Sides.CompareTo(b.Sides));
        }

        public override string ToString() {
            return ToString(0);
        }

        public string ToString(int modifierModifier) {
            OrderDice();
            StringBuilder sb = new StringBuilder();
            foreach (Dice d in this.DiceList) {
                sb.Append(d.ToString())
                  .Append(" + ");
            }
            return sb.Append(Modifier + modifierModifier)
                     .ToString();
        }

    }
}