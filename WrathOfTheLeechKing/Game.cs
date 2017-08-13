using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Game {

        private Random rand;
        private Player player;
        private int currX, currY;
        private List<Room> rooms; // Search using rooms.Find(r => (r.roomX == 0)&&(r.roomY == 0));
        private Room currRoom;
        private List<Enemy> enemies;

        public Game() {
            rand = new Random();
            player = new Player(rand);
            rooms = new List<Room>();
            enemies = new List<Enemy>();
            currX = 0; currY = 0;
            // Generate first room:
            currRoom = Room.GenerateRoom(rand, currX, currY);
            rooms.Add(currRoom);
            // Display room on screen
            Console.WriteLine();
            DisplayRoomInfo(true);
            // Ready to go?
        }

        public bool DoLogic() {
            // Read command:
            bool readingCmd = true;
            bool beVerbose = false;
            while (readingCmd) {
                Console.Write("Command>>");
                string cmd = Console.ReadLine();
                string[] splicedCmd = cmd.ToLower().Split(' ');
                if (splicedCmd.Length > 0) { // A command of some kind was entered
                    switch (splicedCmd[0]) {
                        case "attack":
                            if (splicedCmd.Length >= 2) { // Check if theres a target.
                                int targetI = enemies.FindIndex(e => (e.EnemyName.ToLower() == splicedCmd[1]));
                                if (targetI >= 0) { // Target is legal
                                    PerformAttack(targetI);
                                    readingCmd = false;
                                    continue;
                                }
                            }
                            break;
                        case "go":
                        case "move":
                            if (splicedCmd.Length >= 2) { // Check if a direction is specified
                                int dirx = 0; int diry = 0;
                                switch (splicedCmd[1]) {
                                    case "n":
                                    case "north":
                                        diry = 1;
                                        break;
                                    case "s":
                                    case "south": 
                                        diry = -1;
                                        break;
                                    case "e":
                                    case "east":
                                        dirx = 1;
                                        break;
                                    case "w":
                                    case "west":
                                        dirx = -1;
                                        break;
                                }
                                if (dirx != 0 || diry != 0) {
                                    if (enemies.Count() > 0) { // Can't move while enemies are present
                                        Console.WriteLine("You cant just run away like that!");
                                        continue;
                                    } else { // Do movement
                                        MovePlayer(dirx, diry);
                                        readingCmd = false;
                                        beVerbose = true;
                                        continue;
                                    }
                                }
                            }
                            break;
                        case "examine":
                        case "look":
                            beVerbose = true;
                            readingCmd = false;
                            continue;
                        case "help":
                        case "?":
                            Console.WriteLine("Attack noun to attack, move direction to move, look to look around");

                            break;
                    }
                }
                Console.WriteLine("! Didnt understand that command. Type help for help.");
            }
            Console.WriteLine();
            DisplayRoomInfo(beVerbose);
            return true;
        }

        

        private void DisplayRoomInfo(bool verbose) {
            Console.WriteLine("Room " + currX + ";" + currY);
            if (verbose) Console.WriteLine(currRoom.ToString());
            if (enemies.Count() > 0) { // Enemies exist in room
                Console.WriteLine("-----Foes-----");
                DisplayEnemyInfo();
            }
        }

        private void DisplayEnemyInfo() {
            foreach (Enemy e in enemies) {
                Console.WriteLine(">> " + e.ToString() + " <<>> HP: " + e.CurrHp + "/" + e.MaxHP + " <<");
            }
        }

        private void MovePlayer(int x, int y) {
            currX += x; currY += y;
            // Check if new room already exists:
            int roomIndex = rooms.FindIndex(r => (r.RoomX == currX) && (r.RoomY == currY));
            if (roomIndex == -1) { // Room doesnt yet exist, create it
                CreateNewRoomHere();
            } else { // Room exists, load it
                currRoom = rooms[roomIndex];
            }
        }

        private void CreateNewRoomHere() {
            currRoom = Room.GenerateRoom(rand, currX, currY);
            rooms.Add(currRoom);
            // Chance of an enemy and stuff
            int ch = rand.Next(0,99);
            if (ch > 60) { // 40%~
                enemies = Enemy.GenerateEnemyParty(HelperClass.CalcDifficulty(currX, currY, rooms.Count()), rand);
            }
        }

        private void PerformAttack(int targetIndex) {
            Enemy e = enemies[targetIndex];
            Console.Write("\nYou attack " + e.ToString() + " with your " + player.eWeapon.WpnName + "!");
            Console.Write("\nChance to hit: " + HelperClass.CalcPlayerChanceToHit(player, e) + "% >");
            if (HelperClass.PlayerAttackHitConfirm(player, e)) {
                Console.Write("Hit!\n");
                Console.Write("Damage roll [" + HelperClass.PlayerAttackRollString(player, e) + "]");
                int damage = HelperClass.PlayerAttackCalc(player, e);
                Console.Write(" >Dealt " + damage + " damage to " + e.EnemyName);
                bool enemyDead = e.DealDamage(damage);
                if (enemyDead) {
                    Console.WriteLine("\n" + e.ToString() + " dies..");
                    enemies.RemoveAt(targetIndex);
                }
            } else {
                Console.Write("Miss!\n");
            }
            Console.WriteLine();
        }

    }
}
