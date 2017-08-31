using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class WeaponData {


        public static Weapon GenerateWeapon(int difficulty, Random rand) {
            WeaponDataStruct wep = coreWeapons[rand.Next(0, coreWeapons.Length)];
            EnchantmentDataStruct pre = prefixEnchantments.Values.ElementAt(rand.Next(0, prefixEnchantments.Count()));
            EnchantmentDataStruct sue = suffixEnchantmentsBasic[rand.Next(0, suffixEnchantmentsBasic.Length)];
            int tier = 0;
            int acc = wep.accuracy;
            int critC = wep.critChance;
            int critD = wep.critDamagePercent;
            string name = pre.name + wep.name + sue.name;
            DiceSet dmgd = wep.damageDice;
            List<Game.DamageElements> dmgE = new List<Game.DamageElements>();
            List<WeaponEffects> weffs = new List<WeaponEffects>();
            int[] diceByTier = { 4, 6, 8, 10, 12};

            IEnumerable<WeaponEffectStruct> es = pre.effects.Union(sue.effects);
            foreach(WeaponEffectStruct eff in es) {
                switch (eff.effect) {
                    case WeaponEffects.Terror:
                        weffs.Add(WeaponEffects.Terror);
                        break;
                    case WeaponEffects.Bleed:
                        weffs.Add(WeaponEffects.Bleed);
                        break;
                    case WeaponEffects.DamageModInc:
                        dmgd.Modifier += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.DamageDiceInc:
                        dmgd.AddDice(new Dice(int.Parse(eff.values[0]), int.Parse(eff.values[1])));
                        break;
                    case WeaponEffects.DamageDiceByTier:
                        dmgd.AddDice(new Dice(int.Parse(eff.values[0]), diceByTier[tier]));
                        break;
                    case WeaponEffects.DamageModByTier:
                        dmgd.Modifier += int.Parse(eff.values[0]) * tier;
                        break;
                    case WeaponEffects.AccuracyByTier:
                        acc += int.Parse(eff.values[0]) * tier;
                        break;
                    case WeaponEffects.CritChanceInc:
                        critC += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.CritDamageMultiplierInc:
                        critD += int.Parse(eff.values[0]);
                        break;
                    case WeaponEffects.Element:
                        dmgE.Add((Game.DamageElements)Enum.Parse(typeof(Game.DamageElements), eff.values[0]));
                        break;
                    case WeaponEffects.AccuracyInc:
                        acc += int.Parse(eff.values[0]);
                        break;
                    default:
                        break;
                }
            }

            // Tier:
            // +5 acc per tier and +2 DmgMod
            acc += tier * 5;
            dmgd.Modifier += tier * 2;

            // Make sure element isnt empty
            if (dmgE.Count == 0) {
                dmgE.Add(Game.DamageElements.None);
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
            DamageModInc, DamageDiceInc, CritChanceInc, CritDamageMultiplierInc,
            Element, AccuracyInc, DamageDiceByTier, DamageModByTier, AccuracyByTier,
            Terror, Bleed, Weaken
        }
        /*
         * Special weapone effects:
         * Terror - On attack, 30% to inflict "terrified"
         *          While terrified, enemies have -40 to hit chance.
         * Weaken - On attack, 30% to inflict "weakened"
         *          While weakened, enemies deal 25% less damage.
         * Bleed - On crit, inflict "bleeding"
         *         While bleeding, enemies take damage equal to 1/10 of their HP after they perform an action.
         */

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
            new WeaponDataStruct("War Axe", 64, 3,
                            new DiceSet(new Dice[] {new Dice(1, 6), new Dice(2, 12)}, 1)),
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

        private static EnchantmentDataStruct[] suffixEnchantmentsBasic = {
            new EnchantmentDataStruct(" of the Duelist", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"})
            }),
            new EnchantmentDataStruct(" of the Berserker", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"25"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"-5"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "8"})
            }),
            new EnchantmentDataStruct(" of the Sky", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"7"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"10"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"-1"})
            }),
            new EnchantmentDataStruct(" of Gales", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"5"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"30"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"-1"})
            }),
            new EnchantmentDataStruct(" of Terror", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.Terror, new string[]{"0"})
            }),
            new EnchantmentDataStruct(" of the Assassin", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.Bleed, new string[]{"0"})
            }),
            new EnchantmentDataStruct(" of the Ravager", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.Bleed, new string[]{"0"})
            }),
            new EnchantmentDataStruct(" of the Snake", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Weaken, new string[]{"0"})
            }),
            new EnchantmentDataStruct(" of Mystery", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "3"})
            }),
            new EnchantmentDataStruct(" of Might", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "8"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"-2"}),
            }),
            new EnchantmentDataStruct(" of the Joker", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"1"}),
            }),
        };

        private static EnchantmentDataStruct[] suffixEnchantmentsAdvanced = {
            new EnchantmentDataStruct(" of Subtlety", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"3"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"20"}),
            }),
            new EnchantmentDataStruct(" of Salt", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Salt"}),
            }),
        };

        private static Dictionary<string, EnchantmentDataStruct> prefixEnchantments = new Dictionary<string, EnchantmentDataStruct>() {
            {
            "Flaming", new EnchantmentDataStruct("Flaming ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Heat"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Freezing", new EnchantmentDataStruct("Freezing ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Cold"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Corroding", new EnchantmentDataStruct("Corroding ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Corrode"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Shocking", new EnchantmentDataStruct("Shocking ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Shock"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Eldritch", new EnchantmentDataStruct("Eldritch ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.Element, new string[]{"Calamity"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Vorpal", new EnchantmentDataStruct("Vorpal ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"4"}),
                                      new WeaponEffectStruct(WeaponEffects.CritDamageMultiplierInc, new string[]{"100"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModByTier, new string[]{"1"})
            })}, {
            "Rending", new EnchantmentDataStruct("Rending ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceByTier, new string[]{"1"})
            })}, {
            "Precision", new EnchantmentDataStruct("Precision ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"6"})
            })}, {
            "Dwarven", new EnchantmentDataStruct("Dwarven ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"1"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"4"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageDiceInc, new string[]{"1", "8"})
            })}, {
            "Elven", new EnchantmentDataStruct("Elven ", new WeaponEffectStruct[] {
                                      new WeaponEffectStruct(WeaponEffects.AccuracyInc, new string[]{"9"}),
                                      new WeaponEffectStruct(WeaponEffects.DamageModInc, new string[]{"2"}),
                                      new WeaponEffectStruct(WeaponEffects.CritChanceInc, new string[]{"4"})
            }) }
        };

        private static List<string[]> prefixEnchantmentsSelector = new List<string[]>(){
            // Some enchantments have higher chance then others, so they just appear more in the list
            new string[] { // Tier 0
                "Flaming", "Flaming", "Freezing", "Freezing",
                "Corroding", "Shocking", "Eldritch",
                "Vorpal", "Vorpal", "Rending", "Rending"
            }, new string[] { // Tier 1

            }, new string[] { // Tier 2

            }, new string[] { // Tier 3

            }, new string[] { // Tier 4

            },
        };


    }
}
