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
        private List<Item> items = new List<Item>();
        public int numberOfItems { get { return items.Count; } }
        public Room(string room)
        {
            roomName = room;
        }

        public void addItem(Item item)
        {
            items.Add(item);
        }

        public List<Item> RoomInv()
        {
            return items;
        }

        public void removeItem(string delItem)
        {
            int index = items.FindIndex(item => item.itemName == delItem);
            if(index == -1)
            {
                Console.WriteLine($"Error: Item {delItem} not found in {roomName}");
                return;
            }
            items.RemoveAt(index);
        }
    }
}
