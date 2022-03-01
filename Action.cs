using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class Action
    {
        public Room room;
        private string type;
        public int amount;
        public Item item;
        public string buttonName;
        public bool canShow;
        public string action = "";

        public Action(Room r, string direction)
        {
            room = r;
            type = direction;
            amount = 0;
            canShow = true;
        }
        public Action(Item i, int amt)
        {
            item = i;
            type = item.itemName;
            amount = amt;
        }

        public Action(Item i, string action)
        {
            item = i;
            type = item.itemName;
            amount = -1;
            this.action = action;
        }

        public string getName()
        {
            if (amount != 0)
            {
                if (item != null)
                {
                    if (amount == -1)
                    {
                        buttonName = item.itemName;
                    } else
                    {
                        buttonName = amount + " " + item.itemName;
                    }
                    return buttonName;
                } else
                {
                    buttonName = amount + " " + type;
                    return buttonName;
                }
            }
            return " ";
        }

        public string getAction()
        {
            if (action != "")
            {
                if (action == "Light")
                {
                    if (item.isLit)
                    {
                        return "Extinguish " + item.itemName;
                    } else
                    {
                        return "Light " + item.itemName;
                    }
                }
                return action + " " + item.itemName;
            }
            if (item != null)
            {
                if (item.isInteractable)
                {
                    return item.interactionName + " " + item.itemName;
                }
                return "Collect " + item.itemName;
            } else if (room != null)
            {
                return "Go " + type;
            }
            return "Tiene una problema";
        }

        public Room getRoom()
        {
            return room;
        }

        public void setRoom(Room r)
        {
            room = r;
        }

        public bool findCanShow(int index)
        {
            if (index >= 1) return true;
            if (index == -1)
            {
                if (action == "")
                {
                    int amt = room.gm.player.checkInventory(new Item("Gas"));
                    return amt != -1;// Returns true if player has gas, else returns false;
                } 
                else
                {
                    return item.furnaceFuel > 0;
                }
            }
            return true;
        }

        public bool findCanShow()
        {
            canShow = findCanShow(item.id);
            return canShow;
        }

        public void doAction(int i)
        {

        }
    }
}
