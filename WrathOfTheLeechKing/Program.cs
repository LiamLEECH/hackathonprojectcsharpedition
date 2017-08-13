using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Program {
        private static Random rand = new Random();
        static void Main(string[] args) {

            Console.WriteLine("Type commands: s - start, w - get 3 random weapons, r - get 1 random room, e - get 1 random enemy party");
            bool continueLoop = true;
            while (continueLoop) {
                string input = Console.ReadLine();
                Console.WriteLine();
                switch (input[0]) {
                    case 's':
                        continueLoop = false;
                        break;
                    case 'w':
                        for (int i = 0; i < 3; i++) {
                            Weapon wpn = Weapon.GenerateWeapon(0, rand);
                            Console.WriteLine(wpn.WpnName + " - Damage: " + wpn.DmgDiceNum + "d" + wpn.DmgDiceSides + "+" +
                                              wpn.DmgMod + ". Accuracy: " + wpn.Acc + ".");
                        }
                        break;
                    case 'r':
                        Room room = Room.GenerateRoom(rand, 0, 0);
                        Console.WriteLine(room.ToString());
                        break;
                    case 'e':
                        List<Enemy> ems = Enemy.GenerateEnemyParty(1, rand);
                        foreach (Enemy e in ems) {
                            Console.WriteLine(e.ToString() +
                                              " HP: " + e.MaxHP +
                                              ". Defence: " + e.Defence +
                                              ". Acc: " + e.HitChance + ". Dodge: " + e.DodgeChance +
                                              ". Damage " + e.DamageDice + "d" + e.SidesDice + " + " + e.DamageModifier +
                                              ". Attack type: " + e.AttackType

                            );
                        }
                        break;
                }
                Console.WriteLine();
            }

            // Game Loop
            continueLoop = true;
            Game game = new Game();
            while (continueLoop) {
                continueLoop = game.DoLogic();
            }
        }
    }
}