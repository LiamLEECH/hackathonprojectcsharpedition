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
        private List<string> enemies;
        private Random rand;

        public Game(Player.Races race) {
            rand = new Random();
            player = new Player(race, rand);
            currX = 0;
            currY = 0;
            rooms = new List<Room>();
            // Generate First Room
            currRoom = Room.GenerateRoom(rand, currX, currY);
            rooms.Add(currRoom);
            // Display room on screen
            Console.WriteLine();
            DisplayRoomInfo(true);
            Console.WriteLine();
        }

        public enum DamageTypes {
            None, Heat, Cold, Salt, Corrode, Shock, Noxious
        }


        public bool DoGameAction(bool beVerbose) {
            bool readingCmd = true;
            while(readingCmd) {
                Console.Write(">>");
                string cmd = Console.ReadLine();
                string[] splicedCmd = cmd.ToLower().Split(' ');
                if (splicedCmd.Length > 0) { // A command was entered
                    switch (splicedCmd[0]) {

                    }
                }
                Console.WriteLine("! Didnt understand that command. Type help for help.");
            }
            Console.WriteLine();
            
            return false;
        }

        public static int CalcDifficulty(int x, int y, int numRooms) {
            int d = (x + y + (numRooms/2) / 6);
            if (d < 0) d = 0;
            if (d > 7) d = 7;
            return d;
        }

        private void DisplayRoomInfo(bool verbose) {
            Console.WriteLine("Room " + currX + ":" + currY);
            if (verbose) Console.WriteLine(currRoom.ToString());
            if (enemies.Count() > 0) {
                Console.WriteLine("----FOes----");
                DisplayEnemyInfo();
            }
        }
        private void DisplayEnemyInfo() {
            foreach (Enemy e in enemies) {
                Console.WriteLine(">> " + e.ToString() + " <<>> HP: " + e.CurrHp + "/" + e.MaxHP + " <<");
            }
        }

    }
}
