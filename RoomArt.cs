using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class RoomArt
    {
        public string startRoom;
        public string unsearchedRoom;

        public RoomArt()
        {
            unsearchedRoom = getUnsearchedRoomArt();
        }

        public string buildArt(Room r)
        {
            List<string> building = new List<string>();
            List<string> back = buildBackWall(r);
            List<string> left = buildLeftWall(r);
            List<string> right = buildRightWall(r);
            string toReturn = "";
            for (int i = 0; i < back.Count; i++)
            {
                string toAdd = left[i] + back[i] + right[i];
                building.Add(toAdd);
            }
            if (r.getRoomName() == "First Room")
            {
                List<string> copy = new List<string>(building);
                foreach (Tuple<int, Item> content in r.contents)
                {
                    if (content.Item2.id == -1)
                    {
                        string hold;
                        for (int i = 1; i < 10; i++)
                        {
                            hold = building[i];
                            hold = hold.Remove(12, 2);
                            hold = hold.Insert(12, "||");
                            building[i] = hold;
                        }
                        hold = building[10];
                        hold = hold.Remove(12, 6);
                        hold = hold.Insert(12, "||____");
                        building[10] = hold;

                        hold = building[11];
                        hold = hold.Remove(11, 8);
                        hold = hold.Insert(11, "/------\\");
                        building[11] = hold;

                        hold = building[12];
                        hold = hold.Remove(11, 8);
                        hold = hold.Insert(11, "| .--. |");
                        building[12] = hold;


                        if (content.Item2.isLit)
                        {
                            hold = building[13];
                            hold = hold.Remove(11, 8);
                            hold = hold.Insert(11, "| |##| |");
                            building[13] = hold;

                            hold = building[14];
                            hold = hold.Remove(11, 8);
                            hold = hold.Insert(11, "| |##| |");
                            building[14] = hold;
                        }
                        else
                        {
                            hold = building[13];
                            hold = hold.Remove(11, 8);
                            hold = hold.Insert(11, "| |  | |");
                            building[13] = hold;

                            hold = building[14];
                            hold = hold.Remove(11, 8);
                            hold = hold.Insert(11, "| |--| |");
                            building[14] = hold;
                        }

                        hold = building[15];
                        hold = hold.Remove(11, 8);
                        hold = hold.Insert(11, "| ```` |");
                        building[15] = hold;

                        hold = building[16];
                        hold = hold.Remove(11, 8);
                        hold = hold.Insert(11, "\\______/");
                        building[16] = hold;
                    }
                }
            }
            for (int i = 0; i < building.Count; i++)
            {
                toReturn += building[i] + "\n";
            }
            return toReturn;
        }

        private List<string> buildBackWall(Room r)
        {
            List<string> wall = new List<string>();
            bool hasRoom = (r.getRoom("up") != null);
            wall.Add("____________________________________");
            wall.Add("                                    ");
            wall.Add("____________________________________");

            if (!hasRoom)
            {
                for (int i = 0; i < 12; i++)
                {
                    wall.Add("                                    ");
                }
                wall.Add("____________________________________");
            } else
            {
                wall.Add("                                    ");
                wall.Add("             __________             ");
                wall.Add("            |  __  __  |            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |__||__| |            ");
                wall.Add("            |  __  __()|            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |  ||  | |            ");
                wall.Add("            | |__||__| |            ");
                wall.Add("____________|__________|____________");
            }
            for (int i = 0; i < 3; i++)
            {
                wall.Add("                                    ");
            }
            wall.Add("____________________________________");
            return wall;
        }

        private List<string> buildLeftWall(Room r)
        {
            List<string> wall = new List<string>();
            bool hasDoor = (r.getRoom("left") != null);
            if (r.ran1)
            {
                wall.Add(" ____________");
                wall.Add("|'`--...___  ");
                wall.Add("|          '.");
                wall.Add("|      _,-''|");
                wall.Add("|  ,,-'     |");
                wall.Add("|''         |");
                wall.Add("|           |");
                wall.Add("|           |");
                if (hasDoor)
                {
                    wall.Add("|      ,|   |");
                } else
                {
                    wall.Add("|           |");
                }
            } else
            {
                wall.Add(" ____________");
                wall.Add("|'`--...___  ");
                wall.Add("|          '.");
                wall.Add("|      _,-''|");
                wall.Add("|  ,,-'   | |");
                wall.Add("|''      () |");
                wall.Add("|  ,   ('   |");
                wall.Add("| |, ('     |");
                if (hasDoor)
                {
                    wall.Add("| |    ,|   |");
                }
                else
                {
                    wall.Add("| |         |");
                }
            }
            if (hasDoor)
            {
                wall.Add("|    .' |   |");
                for (int i = 0; i < 5; i++)
                {
                    wall.Add("|    |  |   |");
                }
                wall.Add("|    |o |   |");
                wall.Add("|    |  |  / ");
                wall.Add("|    |  | /  ");
                wall.Add("|    |  |/   ");
                wall.Add("|____|__/____");
            } else
            {
                for (int i = 0; i < 7; i++)
                {
                    wall.Add("|           |");
                }
                wall.Add("|          / ");
                wall.Add("|         /  ");
                wall.Add("|        /   ");
                wall.Add("|_______/____");
            }
            return wall;
        }

        private List<string> buildRightWall(Room r)
        {
            List<string> wall = new List<string>();
            bool hasRoom = (r.getRoom("right") != null);

            wall.Add("______________ ");
            wall.Add("     __,,...--|");
            wall.Add(".-'''         |");
            wall.Add("|`-.._        |");
            wall.Add("|     ``-.._  |");
            wall.Add("|           `-|");
            
            if (hasRoom)
            {
                wall.Add("|             |");
                wall.Add("| |`.         |");
                for (int i = 0; i < 3; i++)
                {
                    wall.Add("| |  |        |");
                }
                wall.Add("| o  |        |");
                for (int i = 0; i < 4; i++)
                {
                    wall.Add("| |  |        |");
                }
                wall.Add(" \\|  |        |");
                wall.Add("  \\  |        |");
                wall.Add("   \\ |        |");
            } else
            {
                for (int i = 0; i < 10; i++)
                {
                    wall.Add("|             |");
                }
                wall.Add(" \\            |");
                wall.Add("  \\           |");
                wall.Add("   \\          |");
            }
            wall.Add("____\\_________|");
            if (r.ran2)
            {
                //10 - 15
                string hold = wall[10];
                hold = hold.Remove(8, 4);
                hold = hold.Insert(8, "|`-.");
                wall[10] = hold;

                hold = wall[11];
                hold = hold.Remove(8, 4);
                hold = hold.Insert(8, "|/\\|");
                wall[11] = hold;

                hold = wall[12];
                hold = hold.Remove(8, 4);
                hold = hold.Insert(8, "|`.|");
                wall[12] = hold;

                hold = wall[13];
                hold = hold.Remove(8, 4);
                hold = hold.Insert(8, "|\\/|");
                wall[13] = hold;

                hold = wall[14];
                hold = hold.Remove(9, 3);
                hold = hold.Insert(9, "`.|");
                wall[14] = hold;

                hold = wall[15];
                hold = hold.Remove(11, 1);
                hold = hold.Insert(11, "'");
                wall[15] = hold;
            } //Randomly adds a picture to the wall
            return wall;
        }

        private string getUnsearchedRoomArt()
        {
            string strg = "";
            strg += " ______________________________________________________________ \n";
            strg += "|'`--...~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~,,...--|\n";
            strg += "|~~~~~~~~~~'.____________________________________.-'''~~~~~~~~~|\n";
            strg += "|~~~~~~_,-''|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|`-.._~~~~~~~~|\n";
            strg += "|~~,,-'~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~``-.._~~|\n";
            strg += "|''~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~`-|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~~|____________________________________|~~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~~/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\~~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~~/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\~~~~~~~~~~~|\n";
            strg += "|~~~~~~~~/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\\~~~~~~~~~~|\n";
            strg += "|_______/____________________________________________\\_________|\n";
            return strg;
        }

        private static readonly DateTime Jan1st1970 = new DateTime
    (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public static int CurrentTimeMillis()
        {
            return (int)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }

    
}
