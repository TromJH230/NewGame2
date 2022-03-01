using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class Item
    {
        public static readonly Item Wood = new Item("Wood");
        public static readonly Item Cloth = new Item("Cloth");
        public static readonly Item Gas = new Item("Gas");
        public bool isConsumable;
        public bool isCollectable;
        public bool isInteractable;
        public string itemName;
        public int id = 0;
        public int furnaceFuel;
        public string interactionName;
        public bool isLit = false;
        public Item(string item)
        {
            this.itemName = item;
            isConsumable = false;
            isCollectable = true;
            isInteractable = false;
            id = getItemId(item);
        }

        public Item(string item, bool collectable, bool interactable)
        {
            this.itemName = item;
            isConsumable = false;
            isCollectable = collectable;
            isInteractable = interactable;
            id = getItemId(item);
            interactionName = getInteractionName(item);
        }

        private int getItemId(string wrd)
        {
            if (wrd == "The Furnace") id = -1; //The Furnace == -1

            if (wrd == "Wood") id = 1; //Wood == 1
            if (wrd == "Gas") id = 2; //Gas == 2;
            if (wrd == "Cloth") id = 3; //Cloth == 3;
            return id;
        }

        private string getIdName(int i)
        {
            string name = "unknown";
            if (i == -1) name = "The Furnace";

            if (i == 1) name = "Wood";
            if (i == 2) name = "Gas";
            if (i == 3) name = "Cloth";
            return name;
        }

        private string getInteractionName(string input)
        {
            string output = "unknown";
            if (input == "The Furnace") output = "Fuel";
            return output;
        }
    }
}
