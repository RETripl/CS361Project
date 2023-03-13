using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeInv
{
    public class Item
    {
        public string itemName { get; private set; }
        public string roomName { get; private set; }
        public string homeName { get; private set; }
        public Item(string item)
        {
            itemName = item;
        }
        public Item(string item, string room)
        {
            itemName = item;
            roomName = room;
        }
        public Item( string item, string room, string home)
        {
            itemName = item;
            roomName = room;
            homeName = home;
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
            Console.WriteLine($"\n{itemName} is now in {roomName}.");
        }
        public void changeHome(string newHome)
        {
            string oldName = homeName;
            homeName = newHome;
            Console.WriteLine($"\n{itemName} is now in {roomName}");
        }
    }
}
