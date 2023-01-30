using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInv
{
    internal class Room
    {
        public string roomName { get; private set; }
        public List<Item> items = new List<Item>();
        public int numberOfItems { get; private set; }

        public Room(string room)
        {
            roomName = room;
            numberOfItems = 0;
        }

        public void addItem(Item item)
        {
            items.Add(item);
            numberOfItems += 1;
        }

        public List<Item> RoomInv()
        {
            return items;
        }

        public void removeItem(string delItem)
        {
            int index = items.FindIndex(item => item.itemName == delItem);
            items.RemoveAt(index);
        }
    }
}
