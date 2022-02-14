using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class GameManager
    {
        public GameManager gm;
        private Room start;
        private Room exit;
        public Room[,] map;
        public string[] visualMap;
        public Player player;
        private List<Tuple<Room, int, int>> roomList;
        public Item furnace;
        public RoomArt art;
        private static Random generator = new Random();
        public GameManager()
        {
            art = new RoomArt();
            generateMap();
            player = new Player();
            gm = this;
        }

        public GameManager getGM()
        {
            return this;
        }

        public Room getStart()
        {
            return start;
        }

        private void generateMap()
        {
            map = new Room[10, 10];
            visualMap = new string[10];
            roomList = new List<Tuple<Room, int, int>>();
            start = new Room("First Room", this);
            furnace = new Item("The Furnace", false, true);
            Action furnaceAction = new Action(furnace, -1);
            Action lightFAction = new Action(furnace, "Light");
            furnaceAction.setRoom(start);
            start.addContent(furnace);
            start.addAction(furnaceAction);
            start.addAction(lightFAction);
            start.addContent(2, new Item("Gas"));
            roomList.Add(new Tuple<Room, int, int>(start, 5, 5));
            map[5, 5] = start;
            //Random ran = new Random(CurrentTimeMillis());

            for (int k = 0; k < 3; k++)
            {
                exit = new Room("Exit Room " + k, this);
                int xExit = generator.Next(-5, 5);
                int yExit = generator.Next(-5, 5);

                map[5 + xExit, 5 + yExit] = exit;
                int random = generator.Next(0, 2);
                if (random == 0)
                {
                    for (int i = 0; i < Math.Abs((double)xExit); i++)
                    {
                        int x = 0;
                        if (xExit < 0)
                        {
                            x = 5 + xExit + i;
                        }
                        else
                        {
                            x = 5 + xExit - i;
                        }
                        Room toAdd = new Room(x + ", " + 5, this);
                        roomList.Add(new Tuple<Room, int, int>(toAdd, x, 5));
                        map[x, 5] = toAdd;
                    }
                    for (int i = 1; i < Math.Abs((double)yExit); i++)
                    {
                        int y = 0;
                        if (yExit < 0)
                        {
                            y = 5 + yExit + i;
                        }
                        else
                        {
                            y = 5 + yExit - i;
                        }
                        Room toAdd = new Room((5 + xExit) + ", " + y, this);
                        roomList.Add(new Tuple<Room, int, int>(toAdd, 5 + xExit, y));
                        map[5 + xExit, y] = toAdd;
                    }
                }
                else
                {
                    for (int i = 0; i < Math.Abs((double)yExit); i++)
                    {
                        int y = 0;
                        if (yExit < 0)
                        {
                            y = 5 + yExit + i;
                        }
                        else
                        {
                            y = 5 + yExit - i;
                        }
                        Room toAdd = new Room(5 + ", " + y, this);
                        roomList.Add(new Tuple<Room, int, int>(toAdd, 5, y));
                        map[5, y] = toAdd;
                    }
                    for (int i = 1; i < Math.Abs((double)xExit); i++)
                    {
                        int x = 0;
                        if (xExit < 0)
                        {
                            x = 5 + xExit + i;
                        }
                        else
                        {
                            x = 5 + xExit - i;
                        }
                        Room toAdd = new Room(x + ", " + (yExit + 5), this);
                        roomList.Add(new Tuple<Room, int, int>(toAdd, x, 5 + yExit));
                        map[x, 5 + yExit] = toAdd;
                    }
                }
            } //Basic path from start to exit

            int offShoots = generator.Next(7, 12);
            for (int i = 0; i < offShoots; i++)
            {
                while (true)
                {
                    int ranNum = generator.Next(0, roomList.Count);
                    Tuple<Room, int, int> hold = roomList[ranNum];
                    List<Tuple<int, int>> nextDoor = new List<Tuple<int, int>>();
                    if (hold.Item2 != 0)
                    {
                        if (map[hold.Item2 - 1, hold.Item3] == null) nextDoor.Add(new Tuple<int, int>(hold.Item2 - 1, hold.Item3));
                    }
                    if (hold.Item2 != 9)
                    {
                        if (map[hold.Item2 + 1, hold.Item3] == null) nextDoor.Add(new Tuple<int, int>(hold.Item2 + 1, hold.Item3));
                    }
                    if (hold.Item3 != 9)
                    {
                        if (map[hold.Item2, hold.Item3 + 1] == null) nextDoor.Add(new Tuple<int, int>(hold.Item2, hold.Item3 + 1));
                    }
                    if (hold.Item3 != 0)
                    {
                        if (map[hold.Item2, hold.Item3 - 1] == null) nextDoor.Add(new Tuple<int, int>(hold.Item2, hold.Item3 - 1));
                    }
                    if (nextDoor.Count > 0)
                    {
                        Tuple<int, int> picked = nextDoor[generator.Next(0, nextDoor.Count)];
                        Room newRoom = new Room(picked.Item1 + ", " + picked.Item2, this);
                        map[picked.Item1, picked.Item2] = newRoom;
                        roomList.Add(new Tuple<Room, int, int>(newRoom, picked.Item1, picked.Item2));
                        break;
                    }
                }
            }//Creating random offshoots

            string mapBuild = "";
            for (int i = 0; i < 10; i++)
            {
                string line = "";
                for (int j = 0; j < 10; j++)
                {
                    if (map[j, i] != null)
                    {
                        line += "X";
                        Room r = map[j, i];
                        if (i != 9)
                        {
                            if (map[j, i + 1] != null)
                            {
                                r.setRoom("down", map[j, i + 1]);
                                map[j, i + 1].setRoom("up", r);
                            }
                        }
                        if (j != 9)
                        {
                            if (map[j + 1, i] != null)
                            {
                                r.setRoom("right", map[j + 1, i]);
                                map[j + 1, i].setRoom("left", r);
                            }
                        }
                    } else
                    {
                        line += "O";
                    }
                }
                mapBuild += line + "\n";
            } //Linking rooms to neighbors & generating ASCII Map
            foreach (Tuple<Room, int, int> room in roomList)
            {
                Room r = room.Item1;
                Item cnt;

                int ranNum = generator.Next(-3, 8); //4 out of 16 rolls will have nothing
                r.ran1 = generator.Next(0, 3) == 1;
                r.ran2 = generator.Next(0, 3) == 1;
                r.rArt = art.buildArt(r);


                if (ranNum > 0)
                {
                    Console.WriteLine("Random Num: " + ranNum);
                    for (int i = 0; i < ranNum; i++)
                    {
                        int rNum = generator.Next(1, 16);
                        if (rNum >= 12)
                        {
                            cnt = new Item("Gas");
                        }
                        else if (rNum >= 6)
                        {
                            cnt = new Item("Cloth");
                        } else
                        {
                            cnt = new Item("Wood");
                        }

                        r.addContent(1, cnt);
                    }
                }
            } //Adding random amount of stuff to each room
            //Console.WriteLine(mapBuild);
        }

        private static readonly DateTime Jan1st1970 = new DateTime
(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public static int CurrentTimeMillis()
        {
            return (int)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }
    }
}
