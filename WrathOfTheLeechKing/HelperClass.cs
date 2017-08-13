using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class HelperClass {

        // Has its own random
        public static Random rand = new Random();

        public static void ShuffleList<T>(List<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static int CalcDifficulty (int x, int y, int n) {
            if (x < 0) x = -x;
            if (y < 0) y = -y;
            int t = x + y + (n / 2);
            double r = t;
            r = (Math.Round(r / 5f));
            if (r < 1) r = 1;
            if (r > 8) r = 8;
            return (int) r;
        }

        public static int PlayerAttackCalc(Player player, Enemy enemy) {
            int dmg = 0;
            // Roll dice
            for (int i = 0; i < player.eWeapon.DmgDiceNum; i++) {
                dmg += rand.Next(1, player.eWeapon.DmgDiceSides);
            }
            dmg += player.eWeapon.DmgMod;
            dmg += player.Str;
            dmg -= enemy.Defence;
            if (dmg < 0) dmg = 0;
            return dmg;
        }

        public static string PlayerAttackRollString(Player player, Enemy enemy) {
            return (player.eWeapon.DmgDiceNum + "d" + player.eWeapon.DmgDiceSides +
                " + " + (int)(player.eWeapon.DmgMod + player.Str) + " - Def: " + enemy.Defence
            );
        }

        public static bool PlayerAttackHitConfirm(Player player, Enemy enemy) {
            return (rand.Next(0, 99) < CalcPlayerChanceToHit(player, enemy));
        }

        public static int CalcPlayerChanceToHit(Player player, Enemy enemy) {
            int hitc = player.eWeapon.Acc + (player.Dex * 4);
            hitc -= enemy.DodgeChance;
            if (hitc < 0) hitc = 0;
            if (hitc > 100) hitc = 100;
            return hitc;
        }

    }
}
