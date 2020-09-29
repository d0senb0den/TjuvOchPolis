using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Citizen : Person
    {
        private static Random random = new Random();
        public List<Item> belongings { get; set; }
        public Citizen(Position position, Direction direction, string name) : base(position, direction, name)
        {
            List<Item> starter = new List<Item>();
            starter.Add(new Item("Wallet"));
            starter.Add(new Item("Keys"));
            starter.Add(new Item("Wristwatch"));
            starter.Add(new Item("Money"));
            starter.Add(new Item("Cellphone"));
            belongings = starter;
        }
        public override int GetItemsSize()
        {
            return belongings.Count;
        }
        public override void RemoveItem(Item item)
        {
            belongings.Remove(item);
        }
        public override Item GetRngItem()
        {
            return belongings[random.Next(0, belongings.Count)];
        }

    }
}
