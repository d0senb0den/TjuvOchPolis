using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TjuvOchPolis
{
    class Person
    {
        public Position position { get; set; }
        public Direction direction { get; set; }
        public string name { get; set; }
        public Person(Position position, Direction direction, string name)
        {
            this.position = position;
            this.direction = direction;
            this.name = name;
        }

        public virtual int GetItemsSize()
        {
            return 0;
        }
        public virtual void AddItem(Item item)
        {
        }
        public virtual void RemoveItem(Item item)
        {
        }
        public virtual void RemoveAllItems()
        {
        }
        public virtual Item GetRngItem()
        {
            return null;
        }
        public virtual List<Item> ConfiscateItem()
        {
            return null;
        }
        public virtual int GetPrisonTime()
        {
            return 0;
        }
        public virtual void IncreasePrisonTime()
        {
        }
    }
}
