using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Program {
        static void Main(string[] args) {
            Random rand = new Random();
            Console.WriteLine("Enter command: (s - start, w - generate 3 random weapons, r - generate a random room, n - names test");
            bool continueLoop = true;
            while (continueLoop) {
                Console.WriteLine();
                Console.Write(">>");
                string input = Console.ReadLine();
                if (input.Length < 1) continue;
                switch (input[0]) {
                    case 's':
                        continueLoop = false;
                        break;
                    case 'w':
                        for (int i = 0; i < 3; i++) {
                            GenerateSampleWeapon(0, rand);
                            Console.WriteLine("------------");
                        }
                        break;
                    case 'r':
                        GenerateSampleRoom(rand, 0, 0);
                        Console.WriteLine("------------");
                        break;
                    case 'n':
                        EnemyData.PrintNames(rand);
                        break;
                }
            }
            // Game Loop
            continueLoop = true;
            Game game = new Game(Player.Races.Human);
            while (continueLoop) {
                continueLoop = game.DoGameAction(false);
            }
            Console.ReadLine();
        }

        static void GenerateSampleWeapon(int difficulty, Random r) {
            Weapon wep = WeaponData.GenerateWeapon(difficulty, r);
            Console.WriteLine(wep.Name);
            Console.WriteLine(wep.ToString());
            Console.WriteLine(wep.EffectsToString());
        }

        static void GenerateSampleRoom(Random r, int x, int y) {
            Room room = Room.GenerateRoom(r, x, y);
            Console.WriteLine(room.ToString());
        }
    }
}