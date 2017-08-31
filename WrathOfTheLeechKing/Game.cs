using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Game {

        private int currX, currY;
        private Room currRoom;
        private List<Room> rooms;
        private Player player;
        private List<Enemy> enemies;
        private Random rand;

        public Game(Player.Races race) {
            rand = new Random();
            player = new Player(race, true, rand);
            currX = 0;
            currY = 0;
            rooms = new List<Room>();
            // Generate First Room
            currRoom = Room.GenerateRoom(rand, currX, currY);
            rooms.Add(currRoom);
            enemies = new List<Enemy>();
            // Display room on screen
            Console.WriteLine();
            DisplayRoomInfo(true);
            Console.WriteLine();
        }

        public enum DamageElements {
            None, Heat, Cold, Salt, Corrode, Shock, Calamity
        }
        public enum DamageTypes {
            Phys, Magi
        }
        


        public bool DoGameAction(bool beVerbose) {
            bool readingCmd = true;
            bool quitGame = false;
            while(readingCmd) {
                Console.Write("\n>>");
                string cmd = Console.ReadLine();
                string[] splicedCmd = cmd.ToLower().Split(' ');
                if (splicedCmd.Length > 0) { // A command was entered
                    switch (splicedCmd[0]) {
                        case "attack":
                        case "a":
                        case "atk":
                        case "kill":
                        case "hit":
                            if (splicedCmd.Length >= 2) { // CHeck if theres a target
                                // Make sure target is legal
                                int eIndex = enemies.FindIndex(e => e.Name.ToLower() == splicedCmd[1]);
                                if (eIndex == -1) // Not a legal enemy
                                    Console.WriteLine("No valid target given.");
                                else {
                                    readingCmd = false;
                                    AttackEnemy(eIndex);
                                }
                            } else {
                                Console.WriteLine("No valid target given.");
                            }
                            continue;
                        case "cast":
                        case "magic":
                        case "spell":
                            Console.WriteLine("Spellcasting not implemented yet.");
                            continue;
                        case "go":
                        case "move":
                            if (splicedCmd.Length >= 2) {
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
                                    default:
                                        Console.WriteLine("Invalid direction");
                                        continue;
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
                            // Can examine an enemy, or if you dont name an enemy, will just examine the room.
                            if (splicedCmd.Length >= 2) {
                                int eIndex = enemies.FindIndex(e => e.Name.ToLower() == splicedCmd[1]);
                                if (eIndex == -1) {
                                    beVerbose = true;
                                    readingCmd = false;
                                } else {
                                    ExamineEnemy(eIndex);
                                }
                            } else {
                                beVerbose = true;
                                readingCmd = false;
                            }
                            continue;
                        case "i":
                        case "inventory":
                        case "me":
                            DisplayInventory();
                            continue;
                        case "help":
                        case "?":
                            Console.WriteLine("Idk ay");
                            continue;
                        case "quit":
                        case "suicide":
                            Console.WriteLine("Quitting game...");
                            quitGame = true;
                            readingCmd = false;
                            continue;
                    }
                }
                Console.WriteLine("! Didnt understand that command. Type help for help.");
            }
            Console.WriteLine();
            DisplayRoomInfo(beVerbose);
            
            return !quitGame;
        }

        public static int CalcDifficulty(int x, int y, int numRooms) {
            int d = (x + y + (numRooms/2) / 6);
            if (d < 0) d = 0;
            if (d > 7) d = 7;
            return d;
        }

        private void DisplayRoomInfo(bool verbose) {
            Console.WriteLine("Room " + currX + ":" + currY);
            Console.WriteLine("|> You <||> HP: " + player.CurrHP + "/" + player.MaxHP + " Status: Normal <|");
            if (verbose) Console.WriteLine(currRoom.ToString());
            if (enemies.Count() > 0) {
                Console.WriteLine("----Foes----");
                DisplayEnemyInfo();
            }
        }

        private void DisplayPlayerInfo() {

        }

        private void ExamineEnemy(int eIndex) {

        }

        private void AttackEnemy(int eIndex) {
            Enemy e = enemies[eIndex];
            Console.Write("\nYou attack " + e.ToString() + " with your " + player.EWeapon.Name + ".");
            int finalHitChance = CalcPlayerChanceToHit(e);
            Console.Write("\nChance to hit: " + finalHitChance + "% |>");
            if ((rand.Next(0, 99) < finalHitChance)) { // Attack hits
                // Check crit
                Console.Write("Hit<| Crit Chance: " + player.EWeapon.CritChance + "% |>");
                int damage = PlayerAttackDamageCalc(e);
                if (rand.Next(0, 99) < player.EWeapon.CritChance) { // Crits
                    Console.Write("Crit<|\n");
                    float critMult = player.EWeapon.CritDamagePercent / 100;
                    damage = (int)critMult * damage;
                } else { // No crit
                    Console.Write("No crit<|\n");
                }
                Console.WriteLine("Damage Roll [" + PlayerDamageRollString(e) + "]");
                Console.WriteLine("Damage dealt: " + damage);
                bool enemyDead = e.DealDamage(damage);
                if (enemyDead) {
                    Console.WriteLine(e.ToString() + " has been slain.");
                    enemies.RemoveAt(eIndex);
                }
            } else { // Miss
                Console.Write("Miss<|\n");
            }
            Console.WriteLine();

        }

        private void DisplayInventory() {
            Console.WriteLine("Equipped Weapon: " + player.EWeapon.ToString());
            Console.WriteLine("Weapon effects: " + player.EWeapon.EffectsToString());
            if (player.SecondWeapon != null) {
                Console.WriteLine("Backup Weapon: " + player.SecondWeapon.ToString());
                Console.WriteLine("Weapon effects: " + player.SecondWeapon.EffectsToString());
            } else {
                Console.WriteLine("No Backup Weapon");
            }
            Console.WriteLine("Charms:");
            foreach (Charm c in player.Charms) {
                Console.Write(c.AmountOwned + "x " + c.ToString() + ". ");
            }
        }

        private void MovePlayer(int dirx, int diry) {
            currX += dirx; currY += diry;
            int roomIndex = rooms.FindIndex(r => (r.RoomX == currX) && (r.RoomY == currY));
            if (roomIndex == -1) { // Room doesnt exist so generate it
                CreateNewRoomhere();
            } else {
                currRoom = rooms[roomIndex];
            }
        }

        private void CreateNewRoomhere() {
            currRoom = Room.GenerateRoom(rand, currX, currY);
            rooms.Add(currRoom);
            // Create enemies?
            int ch = rand.Next(0, 99);
            if (ch > 60) {
                enemies = EnemyData.GenerateEnemyParty(rand, 1);
            }
        }

        private void DisplayEnemyInfo() {
            foreach (Enemy e in enemies) {
                Console.WriteLine("|> " + e.ToString() + " <||> HP: " + e.CurrHP + "/" + e.MaxHP + " <|");
            }
        }

        private int CalcPlayerChanceToHit(Enemy enemy) {
            int hitc = player.EWeapon.Accuracy + (player.Dex * 4);
            hitc -= enemy.DodgeChance;
            if (hitc < 0) hitc = 0;
            if (hitc > 100) hitc = 100;
            return hitc;
        }

        private int PlayerAttackDamageCalc(Enemy e) {
            int dmg = player.EWeapon.DamageDice.RollDice(rand) + player.Str;
            dmg -= e.PhysDefence;
            if (dmg < 0) dmg = 0;
            return dmg;
        }

        private string PlayerDamageRollString(Enemy e) {
            return new StringBuilder().Append(player.EWeapon.DamageDice.ToString(player.Str))
                                      .Append(" - Enemy Def: ")
                                      .Append(e.PhysDefence)
                                      .ToString();
        }

    }
}
