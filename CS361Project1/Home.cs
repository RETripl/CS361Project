using HomeInv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS361Project1
{
    internal class Home
    {
        public string homeName { get; private set; }
        private List<Room> Rooms = new List<Room>();
        public int numberOfRooms { get; private set; }

        public Home(string home)
        {
            homeName = home;
            numberOfRooms = 0;
        }
        public void addRoom(Room room)
        {
            Rooms.Add(room);
            numberOfRooms += 1;
        }
        public List<Room> getRooms()
        {
            return Rooms;
        }
        public void removeRoom(string delRoom)
        {
            int index = Rooms.FindIndex(Room => Room.roomName == delRoom);
            if(index == -1)
            {
                return;
            }
            Rooms.RemoveAt(index);
        }
    }
}

