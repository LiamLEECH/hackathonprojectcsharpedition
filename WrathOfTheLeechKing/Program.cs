using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Program {
        static void Main(string[] args) {
            Random r = new Random();
            DiceSet ds = new DiceSet();
            ds.DiceList.Add(new Dice(2, 6));
            ds.DiceList.Add(new Dice(1, 10));
            ds.Modifier = 2;
            Console.WriteLine(ds.ToString());
            Console.WriteLine("Result: " + ds.RollDice(r));
            Console.ReadLine();
        }
    }
}
