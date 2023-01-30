using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInv
{
    internal class Item
    {
        public string itemName { get; private set; }
        public string roomName { get; private set; }

        public Item(string item)
        {
            itemName = item;
        }
        public Item(string item, string room)
        {
            itemName = item;
            roomName = room;
        }
        public void changeItem(string newName)
        {
            string oldName = itemName; 
            itemName = newName;
            Console.WriteLine($"\n{oldName} is now called {itemName}.");
        }
        public void changeRoom(string newRoom)
        {
            string oldName = roomName;
            roomName = newRoom;
            Console.WriteLine($"\n{oldName} is now called {roomName}.");
        }

        
    }
}
