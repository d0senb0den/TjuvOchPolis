using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Police : Person
    {
        public List<Item> confiscatedItems { get; set; }
        public Police(Position position, Direction direction, string name, List<Item> confiscatedItems) : base(position, direction, name)
        {
            this.confiscatedItems = confiscatedItems;
        }
        
        public override int GetItemsSize()
        {
            return confiscatedItems.Count;
        }
        public override void AddItem(Item item)
        {
            confiscatedItems.Add(item);
        }

    }
}
