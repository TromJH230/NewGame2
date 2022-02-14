using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewGame1
{
    public class Room
    {
        public GameManager gm;
        private string roomName;
        public List<Action> actions;
        private Room upRoom;
        private Room downRoom;
        private Room leftRoom;
        private Room rightRoom;
        private int waitTime = 500;
        private bool searched = false;
        public bool noticed = false;
        private MessageManager mm;
        public List<Tuple<int, Item>> contents;
        public string rArt;
        public bool ran1 = false;
        public bool ran2 = false;
        public Room(string test, GameManager gm)
        {
            actions = new List<Action>();
            contents = new List<Tuple<int, Item>>();
            mm = new MessageManager(this);
            if (test == "First Room")
            {
                mm.addUniqueMessage("You awaken with a pounding headache.\n", waitTime, 0, true);
                mm.addUniqueMessage("You can't remember who or where you are.\n", waitTime, 1, false);
                mm.addUniqueMessage("Next to you is a furnace.\n", waitTime, 2, false);
                mm.addUniqueMessage("It looks like it hasn't been used in a while.\n", 1, 3, false);
                //mm.createOrder1();
            }
            this.gm = gm;
            roomName = test;
        }
        public Room(string test, Room up, Room down, Room left, Room right, GameManager gm)
        {
            actions = new List<Action>();
            contents = new List<Tuple<int, Item>>();
            this.gm = gm;
            roomName = test;
            mm = new MessageManager(this);
            if (test == "First Room")
            {
                mm.addUniqueMessage("You awaken with a pounding headache.\n", waitTime, 0, true);
                mm.addUniqueMessage("You can't remember who or where you are.\n", waitTime, 1, false);
                mm.addUniqueMessage("Next to you is a furnace.\n", waitTime, 2, false);
                mm.addUniqueMessage("It looks like it hasn't been used in a while.\n", 1, 3, false);
                //mm.createOrder1();
            }
            upRoom = up;
            if (up != null)
            {
                up.setRoom("down", this);
                actions.Add(new Action(up, "up"));
            }
            downRoom = down;
            if (down != null)
            {
                down.setRoom("up", this);
                actions.Add(new Action(down, "down"));
            }
            leftRoom = left;
            if (left != null)
            {
                left.setRoom("right", this);
                actions.Add(new Action(left, "left"));
            }
            rightRoom = right;
            if (right != null)
            {
                right.setRoom("left", this);
                actions.Add(new Action(right, "right"));
            }
        }
        
        //Unique Message replaces the "You entered the room." message
        public void addUniqueMessage(List<string> msg, int wait, int position, bool replace)
        {
            mm.addUniqueMessage(msg, wait, position, replace);
        }

        public List<Action> getActions()
        {

            return actions;
        }

        public void addAction(Action toAdd)
        {
            actions.Add(toAdd);
        }
        public Room getRoom(string keyWord)
        {
            Room toReturn = null;
            if (keyWord.Equals("up"))
            {
                toReturn = upRoom;
            }
            else if (keyWord.Equals("down"))
            {
                toReturn = downRoom;
            }
            else if (keyWord.Equals("left"))
            {
                toReturn = leftRoom;
            }
            else if (keyWord.Equals("right"))
            {
                toReturn = rightRoom;
            }
            if (toReturn != null)
            {
                return toReturn;
            }
            else
            {
                return null;
            }
        }//Lowercase room names
        public void setRoom(string keyWord, Room room)//Lowercase room names
        {
            if (keyWord.Equals("up"))
            {
                upRoom = room;
            }
            else if (keyWord.Equals("down"))
            {
                downRoom = room;
            }
            else if (keyWord.Equals("left"))
            {
                leftRoom = room;
            }
            else if (keyWord.Equals("right"))
            {
                rightRoom = room;
            }
            actions.Add(new Action(room, keyWord));
        }

        public bool isSearched()
        {
            return searched;
        }

        public void doSearch()
        {
            searched = true;
            noticed = true;
            if (upRoom != null) upRoom.noticed = true;
            if (downRoom != null) downRoom.noticed = true;
            if (leftRoom != null) leftRoom.noticed = true;
            if (rightRoom != null) rightRoom.noticed = true;
        }

        public string getRoomName()
        {
            return roomName;
        }

        public List<Tuple<List<string>, int>> getOrder()
        {
            return mm.getOrder();
        }

        public void addContent(int amt, Item cnt)
        {
            bool added = false;
            for (int i = 0; i < contents.Count; i++)
            {
                Tuple<int, Item> content = contents[i];
                if (content.Item2.id == cnt.id)
                {
                    Tuple<int, Item> newContent = new Tuple<int, Item>(content.Item1 + amt, cnt);
                    contents[i] = newContent;
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                contents.Add(new Tuple<int, Item>(amt, cnt));
            }
            added = false;
            if (cnt.isCollectable)
            {
                for (int i = 0; i < actions.Count; i++)
                {
                    Action hold = actions[i];
                    if (hold.item != null)
                    {
                        if (hold.item.id == cnt.id)
                        {
                            hold.amount += amt;
                            actions[i] = hold;
                            added = true;
                            break;
                        }
                    }
                }
                if (!added)
                {
                    actions.Add(new Action(cnt, amt));
                }
            }
        } //Adds the specified contents to the room

        public void addContent(Item cnt)
        {
            bool added = false;
            for (int i = 0; i < contents.Count; i++)
            {
                Tuple<int, Item> content = contents[i];
                if (content.Item2.id == cnt.id)
                {
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                contents.Add(new Tuple<int, Item>(-1, cnt));
            }
        } //Adds a non-stackable content to the room if there isn't already one there

        public void removeContent(int amt, Item cnt)
        {
            Console.WriteLine("Here");
            for (int i = 0; i < contents.Count; i++)
            {
                Tuple<int, Item> content = contents[i];
                if (content.Item2.id == cnt.id)
                {
                    content = new Tuple<int, Item>(content.Item1 - amt, cnt);
                    if (content.Item1 <= 0)
                    {
                        contents.RemoveAt(i);
                        Console.WriteLine("Second");
                        break;
                    }
                    contents[i] = content;
                }
            }
            for (int i = 0; i < actions.Count; i++)
            {
                Action act = actions[i];
                if (act != null)
                {
                    if (act.item != null)
                    {
                        if (act.item.id == cnt.id)
                        {
                            actions[i] = null;
                            break;
                        }
                    }
                }
            }
        } //Removes contents from room and into player inventory

        public void cleanActions()
        {
            List<Action> newActions = new List<Action>();
            foreach (Action a in actions)
            {
                if (a != null)
                {
                    newActions.Add(a);
                }
            }
            actions = newActions;
        }

        public class MessageManager
        {
            private List<string> newRoom;
            private List<string> roomContents;
            private Tuple<int, List<string>> unique;
            private List<Tuple<List<string>, int, int, bool>> uniqueMsgs;
            List<Tuple<List<string>, int>> listOrder;
            private Room room;
            public MessageManager(Room r)
            {
                room = r;
                uniqueMsgs = new List<Tuple<List<string>, int, int, bool>>();
                buildMessages();
            }

            public MessageManager(int i, List<string> s)
            {
                unique = new Tuple<int, List<string>>(i, s);
                uniqueMsgs = new List<Tuple<List<string>, int, int, bool>>();
                buildMessages();
            }
            public List<Tuple<List<string>, int>> getOrder()
            {
                buildMessages();
                createOrder();
                return listOrder;
            }
            public void addUniqueMessage(List<string> msg, int wait, int pos, bool replace)
            {
                uniqueMsgs.Insert(0, new Tuple<List<string>, int, int, bool>(msg, wait, pos, replace));
            }
            public void addUniqueMessage(string msg, int wait, int pos, bool replace)
            {
                uniqueMsgs.Add(new Tuple<List<string>, int, int, bool>(new List<string> { msg }, wait, pos, replace));
            }
            public void createOrder()
            {
                listOrder = new List<Tuple<List<string>, int>>();

                //Entered Room Message
                listOrder.Add(new Tuple<List<string>, int>(newRoom, 10));

                //New Room
                if (!room.isSearched())
                {
                    //New Room Action buttons
                    listOrder.Add(new Tuple<List<string>, int>(new List<string> { "Search Button" }, -1));
                }
                listOrder.Add(new Tuple<List<string>, int>(roomContents, 10));
                listOrder.Add(new Tuple<List<string>, int>(new List<string> { "insert_Actions" }, -1));
                if (uniqueMsgs.Count != 0)
                {
                    for (int i = 0; i < uniqueMsgs.Count; i++)
                    {
                        Tuple<List<string>, int, int, bool> toAdd = uniqueMsgs[i];
                        if (toAdd.Item4)
                        {
                            listOrder[toAdd.Item3] = new Tuple<List<string>, int>(toAdd.Item1, toAdd.Item2);
                        }
                        else
                        {
                            listOrder.Insert(toAdd.Item3, new Tuple<List<string>, int>(toAdd.Item1, toAdd.Item2));
                        }
                    }
                    uniqueMsgs.Clear();
                }
            }
            public void buildMessages()
            {
                List<Action> actions = room.getActions();

                //newRoom
                newRoom = new List<string>();
                newRoom.Add("You entered the room.\n");

                //roomContents
                roomContents = new List<string>();
                string toAdd =  "Room " + room.getRoomName() + " Contains:\n";
                bool nothing = true;

                if (room.contents.Count >= 1)
                {
                    foreach (Tuple<int, Item> content in room.contents)
                    {
                        if (content.Item1 == -1)
                        {
                            toAdd += content.Item2.itemName + "\n";
                        } else
                        {
                            toAdd += content.Item1 + " " + content.Item2.itemName + "\n";
                        }
                    }
                    nothing = false;
                } //Adding Contents
                if (actions.Count > 1) 
                {
                    foreach (Action act in actions)
                    {
                        if (act.getName() != " " && act.item == null)
                        {
                            toAdd += act.getName() + "\n";
                            nothing = false;
                        }
                    }
                    
                } //Adding actions
                if (nothing)
                {
                    toAdd += "seemingly nothing...\n";
                } // If nothing is inside the room
                string rooms = "There are doors that lead:\n";
                if (room.getRoom("up") != null)
                {
                    rooms += "Up\n";
                }
                if (room.getRoom("down") != null)
                {
                    rooms += "Down\n";
                }
                if (room.getRoom("left") != null)
                {
                    rooms += "Left\n";
                }
                if (room.getRoom("right") != null)
                {
                    rooms += "Right\n";
                }
                if (rooms != "There are doors that lead:\n")
                {
                    toAdd += rooms;
                }
                roomContents.Add(toAdd);
            }
        }
    }
}
