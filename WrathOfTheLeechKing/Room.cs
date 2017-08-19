using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrathOfTheLeechKing {
    class Room {
        public Overviews Overview {
            get; set;
        }
        public MinorFeatures MinorFeature1 {
            get; set;
        }
        public MinorFeatures MinorFeature2 {
            get; set;
        }
        public Sensories Sensory {
            get; set;
        }
        public int RoomX {
            get; set;
        }
        public int RoomY {
            get; set;
        }
        public Weapon FloorWeapon {
            get; set;
        }
        public Room(Overviews overview, MinorFeatures minorFeature1, MinorFeatures minorFeature2, Sensories sensory, int x, int y) {
            Overview = overview;
            MinorFeature1 = minorFeature1;
            MinorFeature2 = minorFeature2;
            Sensory = sensory;
            RoomX = x;
            RoomY = y;
            FloorWeapon = null;
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            return sb.Append(DescribeOverview(this.Overview))
                     .Append(DescribeMinorFeature(this.MinorFeature1))
                     .Append(DescribeMinorFeature(this.MinorFeature2))
                     .Append(DescribeSensory(this.Sensory))
                     .ToString();
        }

        /*
         * Static data for creating rooms
         */
        public static Room GenerateRoom(Random rand, int x, int y) {
            Array ovs = Enum.GetValues(typeof(Overviews));
            Overviews ov = (Overviews)ovs.GetValue(rand.Next(ovs.Length));
            Array mfs = Enum.GetValues(typeof(MinorFeatures));
            MinorFeatures mf1 = (MinorFeatures)ovs.GetValue(rand.Next(1, mfs.Length)); // Make sure it cant be None
            // 1/3 chance for a second minor feature
            MinorFeatures mf2 = MinorFeatures.None;
            if (rand.Next(2) == 0) {
                mf2 = (MinorFeatures)ovs.GetValue(rand.Next(1, mfs.Length)); // Make sure it cant be None
            }
            // 50% of a sensory
            Sensories sen = Sensories.None;
            if (rand.Next(1) == 0) {
                Array sens = Enum.GetValues(typeof(Sensories));
                sen = (Sensories)sens.GetValue(rand.Next(sens.Length));
            }

            return new Room(ov, mf1, mf2, sen, x, y);
        }
        public enum Overviews {
            Wide, Armory, Torture, Marble, Dark, White, Metal, Graveyard, Garden, Hall, Circle,
            Library, Tomb, foyer, qut
        }
        public enum MinorFeatures {
            None, Glow, Drip, Slime, Needles, Rock, Fruit, Music, Blood, Rats
        }
        public enum Sensories {
            None, Cold, Dank, Water, Shriek, Hot, Echo, Eerie, Ominous, Hunger, Sleep
        }

        /*
         * Static data for describing rooms
         */
        public static string DescribeOverview(Overviews o) {

            switch (o) {
                case Overviews.Wide:
                    return "The room is wide and open, the floor and walls are made of smooth grey rock. " +
                           "The roof towers far above you with rocky stalactites hanging down.";
                case Overviews.Armory:
                    return "This room looks like an abandonded armoury, there are empty weapon racks along the walls, " +
                           "rusted scrap metal that was probably once swords is scattered around the floor. A thick collection of dust is settled on almost everything.";
                case Overviews.Torture:
                    return "In the center of the room sits a menacing looking torture rack. The walls are lined with jail cells, " +
                           "some dusty skeletons still lying inside.";
                case Overviews.Marble:
                    return "The floor of this room is made of well-polished marble. Beautiful paintings line the walls and a huge wooden dining table sits in the center. " +
                           "Everything looks surprisngly well kept, as if someone or something uses this room often.";
                case Overviews.Dark:
                    return "Everything is dim and its hard to see, but you can just make out what seems to be a human corpse sitting in the corner. You hear small bones crunching underneath your feet.";
                case Overviews.White:
                    return "The room is totally white, although dimly lit. The room has an unfinished feel to it, almost as if whoever designed it was not very creative.";
                case Overviews.Metal:
                    return "The entire room is made of a patchwork of metal and other materials. The floor is rough and rusted.";
                case Overviews.Graveyard:
                    return "Although the room is very dark, you can make out the shape of gravestones in rows throughout the room. You are in an indoor cemetary.";
                case Overviews.Garden:
                    return "The room is filled with plantlife. You are in a garden, but you do not recognise the plants in the room.";
                case Overviews.Hall:
                    return "You are in a hallway, It is long and you can barely make out the exit. This corridor has seen better days";
                case Overviews.Circle:
                    return "The room is a grandacious circular auditorium. It looks like it would have been truly marvelous in its prime.";
                case Overviews.Library:
                    return "The room's walls are lined with books and scrolls, stacked neatly into shelves. This must be some kind of library or repository.";
                case Overviews.Tomb:
                    return "This room seems to resemble a tomb. There is a large stone sarcophagus in the middle, but it is sealed tight.";
                case Overviews.foyer:
                    return "This room looks like a foyer to a large house, however there are no windows. Where the windows should be has been boarded up with old wooden panels.";
                case Overviews.qut:
                    return "This room seems familiar. It is Z-block level 4.";
                default:
                    return "";
            }
        }

        public static string DescribeMinorFeature(MinorFeatures mf) {
            switch (mf) {
                case MinorFeatures.Glow:
                    return " The walls of the room are giving off a faint glow. You find it comforting for some reason.";
                case MinorFeatures.Drip:
                    return " A strange liquid is dripping from the roof. You cannot identify the liquid.";
                case MinorFeatures.Slime:
                    return " The surfaces of this room are covered in a fine layer of slime. It is sticky to the touch.";
                case MinorFeatures.Rock:
                    return " The walls of the room appear to be made of a rough stone, of a type you cannot identify.";
                case MinorFeatures.Rats:
                    return " Tiny rats scurry away from you.";
                case MinorFeatures.Blood:
                    return " There is a faint trail of blood accross the floor.";
                case MinorFeatures.Needles:
                    return " The room is covered in tiny needles, not big enough to hurt you, but unnerving nevertheless.";
                case MinorFeatures.Fruit:
                    return " The floor is covered in rotten fruit.";
                case MinorFeatures.Music:
                    return " You can hear faint music coming from somewhere. The melody reminds you of your childhood, or breakfast.";
                default:
                    return "";
            }
        }

        public static string DescribeSensory(Sensories s) {
            switch (s) {
                case Sensories.Cold:
                    return " The air feels just a little bit chilly.";
                case Sensories.Dank:
                    return " Everything is unpleasantly damp and cold, and your skin starts to feel clammy.";
                case Sensories.Water:
                    return " There is a sound of rushing water far above you.";
                case Sensories.Shriek:
                    return " You hear an incredibly high pitched shrieking noise, but can't quite locate where it came from.";
                case Sensories.Hot:
                    return " The room is uncomfortably hot, and you start to sweat.";
                case Sensories.Echo:
                    return " The room seems to echo more than a room of this size should. You feel uneasy.";
                case Sensories.Eerie:
                    return " The room gives you a an eerie sense of impending doom. You cannot be certain why, but you don't want to stay in this room longer than you must.";
                case Sensories.Ominous:
                    return " This room feels wrong and unnatural.";
                case Sensories.Hunger:
                    return " This room makes you feel hungry.";
                case Sensories.Sleep:
                    return " This room makes you feel tired.";
                default:
                    return "";
            }
        }

    }
}
