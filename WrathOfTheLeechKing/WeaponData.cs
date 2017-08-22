using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class WeaponData {


        public static Weapon GenerateWeapon(int difficulty, Random rand) {
            WeaponDataStruct wep = coreWeapons[rand.Next(0, coreWeapons.Length)];
            EnchantmentDataStruct pre = prefixEnchantmentsZero[rand.Next(0, prefixEnchantmentsZero.Length)];
            EnchantmentDataStruct sue = suffixEnchantmentsZero[rand.Next(0, suffixEnchantmentsZero.Length)];

            int acc = wep.accuracy;
            int critC = wep.critChance;
            int critD = wep.critDamagePercent;
            string name = pre.name + wep.name + sue.name;
            DiceSet dmgd = wep.damageDice;
            Game.DamageElements dmgE = Game.DamageElements.None;
            List<WeaponEffects> weffs = new List<WeaponEffects>();

            IEnumerable<WeaponEffectStruct> es = pre.effects.Union(sue.effects);
            foreach(WeaponEffectStruct eff in es) {
                switch (eff.effect) {
                    case WeaponEffects.Salty:
                        weffs.Add(WeaponEffects.Salty);
                        break;
                    case WeaponEffects.Terror:
                        weffs.Add(WeaponEffects.Terror);
                        break;
                    case WeaponEffects.DamageModInc:
                        dmgd.Modifier += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.DamageDiceInc:
                        dmgd.AddDice(new Dice(int.Parse(eff.values[0]), int.Parse(eff.values[1])));
                        break;
                    case WeaponEffects.CritChanceInc:
                        critC += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.CritDamageMultiplierInc:
                        critD += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.Element:
                        dmgE = (Game.DamageElements)Enum.Parse(typeof(Game.DamageElements), eff.values[0]);
                        break;
                    case WeaponEffects.AccuracyInc:
                        acc += int.Parse(eff.values[0]);
                        break;
                    default:
                        break;
                }
            }

            return new Weapon(name, dmgd, acc, critD, critC, weffs, dmgE);
        }


        private struct WeaponDataStruct {
            public string name;
            public DiceSet damageDice;
            public int accuracy;
            public int critChance;
            public int critDamagePercent;
            public WeaponDataStruct(string name, int accuracy, int critChance, DiceSet damageDice) {
                this.name = name;
                this.damageDice = damageDice;
                this.accuracy = accuracy;
                this.critChance = critChance;
                this.critDamagePercent = 200;
            }
        }
        private struct EnchantmentDataStruct {
            public string name;
            public WeaponEffectStruct[] effects;
            public EnchantmentDataStruct(string name, WeaponEffectStruct[] effects) {
                this.name = name;
                this.effects = effects;
            }
        }

        public enum WeaponEffects {
            Salty, DamageModInc, DamageDiceInc, CritChanceInc, CritDamageMultiplierInc,
            Element, AccuracyInc, Terror
        }

        public struct WeaponEffectStruct {
            public WeaponEffects effect;
            public string[] values;
            public WeaponEffectStruct(WeaponEffects effect, string[] values) {
                this.effect = effect;
                this.values = values;
            }
        }

        private static WeaponDataStruct[] coreWeapons = {
            // Jokerinos
            new WeaponDataStruct("Ladle", 64, 4,
                            new DiceSet(new Dice[] {new Dice(2, 4), new Dice(1, 6)}, 0)),
            new WeaponDataStruct("Femur Bone", 67, 4,
                            new DiceSet(new Dice[] {new Dice(1, 4), new Dice(2, 6)}, 0)),
            // Blades
            new WeaponDataStruct("Dagger", 75, 8,
                            new DiceSet(new Dice[] {new Dice(1, 4), new Dice(2, 6)}, 1)),
            new WeaponDataStruct("Longsword", 80, 4,
                            new DiceSet(new Dice[] {new Dice(3, 8)}, 2)),
            new WeaponDataStruct("Falchion", 72, 6,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(1, 8), new Dice(1, 12)}, 3)),
            new WeaponDataStruct("Rapier", 88, 10,
                            new DiceSet(new Dice[] {new Dice(2, 4), new Dice(2, 6)}, 2)),
            new WeaponDataStruct("Katana", 85, 8,
                            new DiceSet(new Dice[] {new Dice(4, 6)}, 1)),
            new WeaponDataStruct("Scimitar", 74, 4,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(2, 12)}, 1)),
            new WeaponDataStruct("Jitte", 81, 3,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(1, 8), new Dice(1, 10)}, 1)),
            // Heavy Weapons
            new WeaponDataStruct("War Hammer", 60, 5,
                            new DiceSet(new Dice[] {new Dice(1, 10), new Dice(2, 12)}, 1)),
            new WeaponDataStruct("Battle Axe", 63, 3,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(2, 12)}, 2)),
            // Spears
            new WeaponDataStruct("Halberd", 71, 4,
                            new DiceSet(new Dice[] {new Dice(2, 3), new Dice(2, 10)}, 2)),
            new WeaponDataStruct("Naginata", 75, 4,
                            new DiceSet(new Dice[] {new Dice(1, 3), new Dice(1, 10), new Dice(1, 12)}, 2)),
            new WeaponDataStruct("Pike", 74, 4,
                            new DiceSet(new Dice[] {new Dice(1, 3), new Dice(2, 12)}, 2)),
            // Bows
            new WeaponDataStruct("Yumi", 86, 6,
                            new DiceSet(new Dice[] {new Dice(3, 3), new Dice(2, 6)}, 1)),
            new WeaponDataStruct("Crossbow", 84, 4,
                            new DiceSet(new Dice[] {new Dice(4, 3), new Dice(1, 10)}, 1)),
            new WeaponDataStruct("Shortbow", 82, 4,
                            new DiceSet(new Dice[] {new Dice(3, 3), new Dice(1, 4), new Dice(1, 8)}, 1)),
            // Fancy
            new WeaponDataStruct("Kusarigama", 73, 7,
                            new DiceSet(new Dice[] {new Dice(1, 3), new Dice(1, 10), new Dice(1, 12)}, 2)),
            new WeaponDataStruct("Knuckles", 69, 5,
                            new DiceSet(new Dice[] {new Dice(2, 8), new Dice(1, 10)}, 2)),
            new WeaponDataStruct("Battle Staff", 70, 3,
                            new DiceSet(new Dice[] {new Dice(3, 8)}, 2)),
        };


        private static WeaponDataStruct[] epicWeapons = {
            new WeaponDataStruct("Literally Just a Glock", 82, 5,
                            new DiceSet(new Dice[] {new Dice(2, 3), new Dice(3, 12)}, 1)),
            new WeaponDataStruct("Leechblade", 80, 9,
                            new DiceSet(new Dice[] {new Dice(2, 6), new Dice(2, 8)}, 4)),
            new WeaponDataStruct("Zweihaender", 68, 6,
                            new DiceSet(new Dice[] {new Dice(2, 10), new Dice(2, 12)}, 1)),
            new WeaponDataStruct("Karambit Fade", 72, 12,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(2, 8), new Dice(2, 10)}, 1)),
        };

        private static EnchantmentDataStruct[] prefixEnchantmentsZero = {
            new EnchantmentDataStruct("Flaming ", new WeaponEffectStruct[]
                                     {new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Heat"})}),
            new EnchantmentDataStruct("Freezing ", new WeaponEffectStruct[]
                                     {new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Cold"})}),
            new EnchantmentDataStruct("Vorpal ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"5"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"50"})}),
            new EnchantmentDataStruct("Rending ", new WeaponEffectStruct[]
                                     {new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "4"})}),
            new EnchantmentDataStruct("Precision ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"6"})}),
        };

        private static EnchantmentDataStruct[] suffixEnchantmentsZero = {
            new EnchantmentDataStruct(" of the Duelist", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"})}),
            new EnchantmentDataStruct(" of the Berserker", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"25"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"-5"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "8"})}),
            new EnchantmentDataStruct(" of the Sky", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"7"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"10"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"-1"})}),
            new EnchantmentDataStruct(" of Terror", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.Terror, new string[]{"0"})}),
            new EnchantmentDataStruct(" of Mystery", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "3"})}),

        };

        private static EnchantmentDataStruct[] prefixENchantmentsOne = {
            new EnchantmentDataStruct("Flaming ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Heat"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"5"})}),
            new EnchantmentDataStruct("Freezing ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Cold"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"5"})}),
            new EnchantmentDataStruct("Vorpal ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"5"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"5"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"50"})}),
            new EnchantmentDataStruct("Rending ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"5"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "4"})}),
            new EnchantmentDataStruct("Precision ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"11"})}),
        };

        

    }
}
