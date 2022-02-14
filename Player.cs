using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class Player
    {
        public List<Tuple<int, Item>> inventory;
        public Player()
        {
            inventory = new List<Tuple<int, Item>>();
        }

        public void addToInventory(int num, Item itm)
        {
            bool added = false;
            for (int i = 0; i < inventory.Count; i++)
            {
                Tuple<int, Item> content = inventory[i];
                if (content.Item2.id == itm.id)
                {
                    added = true;
                    content = new Tuple<int, Item>(content.Item1 + num, itm);
                    inventory[i] = content;
                    if (content.Item1 == 0)
                    {
                        inventory.Remove(content);
                    }
                    break;
                }
            }
            if (!added)
            {
                inventory.Add(new Tuple<int, Item>(num, itm));
            }
        }

        public int checkInventory(Item itm)
        {
            foreach (Tuple<int, Item> item in inventory)
            {
                if (item.Item2.id == itm.id) return item.Item1;
            }
            return -1;
        } //Checks whether inventory contains item. Returns the amount if it does and -1 if it doesn't

        public string getInventoryMessage()
        {
            string message = "Inventory: \n";
            Tuple<int, Item>[] copy = new Tuple<int, Item>[inventory.Count];
            for (int i = 0; i < inventory.Count; i++)
            {
                copy[i] = inventory[i];
            }

            for (int i = 0; i < copy.Length; i++)
            {
                int smallest = 10;
                Tuple<int, Item> hold;
                for (int j = i + 1; j < copy.Length; j++)
                {
                    if (copy[j].Item2.id < copy[i].Item2.id)
                    {
                        hold = copy[j];
                        copy[j] = copy[i];
                        copy[i] = hold;
                    }
                }
            } //Sorts inventory items from smallest ID to largest

            for (int i = 0; i < copy.Length; i++)
            {
                message += copy[i].Item1 + " " + copy[i].Item2.itemName + "\n";
            }
            if (message == "Inventory: \n") message += "Nothing :(";
            return message;
        }

        public int getAmt(Item itm)
        {
            foreach (Tuple<int, Item> i in inventory)
            {
                if (i.Item2.id == itm.id) return i.Item1;
            }
            return 0;
        }
    }
}
