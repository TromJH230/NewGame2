using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGame1
{
    public class Craftables
    {
        public List<string> test;
        public List<string> crafts;
        public List<List<Tuple<Item, int>>> costs;
        public List<List<(Item item, int amt)>> costs_;


        //public Func<string, List<Tuple<Item, int>>> myF = GetCost;

        public Func<string, List<Tuple<Item, int>>> getF() => this.GetCost;

       

        public Craftables()
        {
            var val = costs_[0][0];
            crafts = new List<string>();
            costs = new List<List<Tuple<Item, int>>>();
            GenerateCraftables();
        }

        private void GenerateCraftables()
        {
            crafts.Add("Torch");
            List<Tuple<Item, int>> addCost = new List<Tuple<Item, int>>();
            addCost.Add(new Tuple<Item, int>(Item.Wood, 2));
            addCost.Add(new Tuple<Item, int>(Item.Cloth, 1));
            costs.Add(addCost);

            crafts.Add("Club");
            addCost = new List<Tuple<Item, int>>();
            addCost.Add(new Tuple<Item, int>(Item.Wood, 3));
            costs.Add(addCost);
        }

        public List<Tuple<Item, int>> GetCost(string s)
        {
            int index = 0;
            for (int i = 0; i < crafts.Count; i++)
            {
                if (crafts[i] == s)
                {
                    index = i;
                    break;
                }
            }
            return costs[index];
        }
    }
}
