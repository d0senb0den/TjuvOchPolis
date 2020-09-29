using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Thief : Person
    {
        public List<Item> stolenGoods { get; set; }

        public int prisonTime { get; set; }

        public Thief(Position position, Direction direction, string name, List<Item> stolenGoods, int prisonTime) : base(position, direction, name)
        {
            this.stolenGoods = stolenGoods;
            this.prisonTime = prisonTime;
        }
        
        public override int GetItemsSize()
        {
            return stolenGoods.Count;
        }
        public override void AddItem(Item item)
        {
            stolenGoods.Add(item);
        }
        public override void RemoveAllItems()
        {
            stolenGoods.Clear();
        }
        public override List<Item> ConfiscateItem()
        {
            return new List<Item> (stolenGoods);
        }
        public override int GetPrisonTime()
        {
            return prisonTime;
        }
        public override void IncreasePrisonTime()
        {
            prisonTime++;
        }
    }
}
