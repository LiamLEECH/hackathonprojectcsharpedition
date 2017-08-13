using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Weapon {

        public string WpnName {
            get; set;
        }
        public int DmgDiceNum {
            get; set;
        }
        public int DmgDiceSides {
            get; set;
        }
        public int DmgMod {
            get; set;
        }
        public int Acc {
            get; set;
        }
        public bool IsSalty {
            get; set;
        }

        public Weapon(string wpnName, int dmgDiceNum, int dmgDiceSides, int dmgMod, int acc, bool isSalty) {
            WpnName = wpnName;
            this.DmgDiceNum = dmgDiceNum;
            this.DmgDiceSides = dmgDiceSides;
            this.DmgMod = dmgMod;
            this.Acc = acc;
            this.IsSalty = isSalty;
        }



        // Static functions to generate weapons:

        public static Weapon GenerateWeapon(int difficulty, Random rand) {
            WeaponData w = basicWeapons[rand.Next(0, basicWeapons.Length)];
            EnchantmentData e = basicEnchantments[rand.Next(0, basicEnchantments.Length)];
            int g = rand.Next(0, grades.Length);

            // Calculate Damage
            // NumDice:
            int nDice = w.diceNum;
            if (e.modType == ModType.DICE) {
                nDice += e.modValue;
            }
            // SidesDice:
            int nSides = w.diceSides;
            // DamageMod:
            int dMod = w.dmgMod + g; // Grade is added to damage modifier
            if (e.modType == ModType.DMG) {
                dMod += e.modValue;
            }
            // Calculate Accuracy
            int acc = w.acc + (g * 3); // Each level of grade increases acc by 3.
            if (e.modType == ModType.ACC) {
                acc += e.modValue;
            }
            // Generate Name
            StringBuilder n = new StringBuilder();
            string name = n.Append(grades[g]) // Grade
                           .Append(w.weaponName)
                           .Append(" of ")
                           .Append(e.enchantmentName)
                           .ToString();

            return new Weapon(name, nDice, nSides, dMod, acc, false);
        }

        /*
         * Weapon data
         */
        private static string [] grades = new string [] {
            "Rusty ", "Old ", "", "Fine ", "Superior ", "Exceptional ",
            "Masterwork ", "Legendary ", "Memeful "
        };
        private struct WeaponData {
            public string weaponName;
            public int diceNum;
            public int diceSides;
            public int dmgMod;
            public int acc;
            public WeaponData(string weaponName, int diceNum, int diceSides, int dmgMod, int acc) {
                this.weaponName = weaponName;
                this.diceNum = diceNum;
                this.diceSides = diceSides;
                this.dmgMod = dmgMod;
                this.acc = acc;
            }
        }
        private static WeaponData[] basicWeapons = {
            new WeaponData("Knife", 1, 6, 0, 65),
            new WeaponData("Dagger", 1, 6, 0, 70),
            new WeaponData("Rapier", 2, 6, 0, 90),
            new WeaponData("Cutlass", 1, 10, 1, 80),
            new WeaponData("Longsword", 1, 10, 0, 85),
            new WeaponData("Greatsword", 2, 8, 1, 65),
            new WeaponData("Hatchet", 1, 4, 1, 65),
            new WeaponData("Sickle", 1, 8, 1, 75),
            new WeaponData("War Axe", 1, 10, 2, 70),
            new WeaponData("Wooden Spoon", 1, 4, 0, 55),
            new WeaponData("Ladle", 1, 3, 0, 60),
            new WeaponData("Quarterstaff", 2, 4, 1, 70),
            new WeaponData("Battle Staff", 2, 4, 3, 70),
            new WeaponData("Shortbow", 3, 3, 1, 90),
            new WeaponData("Sling", 2, 3, 1, 80)
        };
        private static WeaponData[] rareWeapons = {
            new WeaponData("Beet", 1, 3, 0, 45),
            new WeaponData("Prosthetic Leg", 1, 4, 0, 43),
            new WeaponData("Wizard's Staff", 2, 4, 4, 70),
            new WeaponData("Great Axe", 1, 20, 0, 60),
            new WeaponData("War Bow", 3, 3, 4, 90),
            new WeaponData("Crossbow", 3, 4, 0, 85)
        };
        private static WeaponData[] mythicWeapons = {
            new WeaponData("Salted Leech", 3, 6, 1, 80),
            new WeaponData("Mr. Sword", 2, 8, 3, 85),
            new WeaponData("A Glock or Something?", 2, 10, 2, 90)
        };
        private enum ModType {
            DMG, ACC, DICE
        }
        private struct EnchantmentData {
            public string enchantmentName;
            public ModType modType;
            public int modValue;
            public EnchantmentData(string enchantmentName, ModType modType, int modValue) {
                this.enchantmentName = enchantmentName;
                this.modType = modType;
                this.modValue = modValue;
            }
        }
        private static EnchantmentData[] basicEnchantments = {
            new EnchantmentData("Slashing", ModType.DMG, 1),
            new EnchantmentData("Crushing", ModType.DMG, 1),
            new EnchantmentData("Politeness", ModType.DMG, -1),
            new EnchantmentData("Sneaking", ModType.ACC, 8),
            new EnchantmentData("Mystery", ModType.ACC, 5),
            new EnchantmentData("Glowyness", ModType.ACC, 3)
        };
        private static EnchantmentData[] fancyEnchantments = {
            new EnchantmentData("Conundrums", ModType.DMG, 1),
            new EnchantmentData("Clumsiness", ModType.ACC, -15),
            new EnchantmentData("Precision", ModType.ACC, 15),
            new EnchantmentData("Sharpness", ModType.DMG, 2),
            new EnchantmentData("Rending", ModType.DMG, 3),
            new EnchantmentData("Uselessness", ModType.DMG, -2),
            new EnchantmentData("Tooting", ModType.ACC, -2),
            new EnchantmentData("Salt", ModType.DMG, 0)
        };

        
    }
}
