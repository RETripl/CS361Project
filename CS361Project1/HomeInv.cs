using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace HomeInv
{
    public class HomeInv
    {
        private List<Room> rooms = new List<Room>();
        private List<Item> Items = new List<Item>();

        public HomeInv()
        {
            Console.WriteLine("Thanks for downloading the newest version!\n" +
                              "You can now follow the menu prompts to add\n" +
                              "or remove and item and add new rooms to your \n" +
                              "home\n" +
                              "\n" +
                              "Press Enter");
            Console.ReadLine();
            bool running = true;
            while (running)
            {
                int choice = getUserChoice();
                switch (choice)
                {
                    case 0:
                        helpMenu();
                        break;
                    case 1:
                        addItem();
                        break;

                    case 2:
                        addRoom();
                        break;
                    case 3:
                        deleteItem();
                        break;
                    case 4:
                        advancedOptions();
                        break;
                    case 10:
                        running = false;
                        break;
                }
            }
        }
        

        private int getUserChoice()
        {
            Console.WriteLine("--Press 0 to access the help menu\n" +
                              "--Press 1 to add an Item \n" +
                              "--Press 2 to add a Room \n" +
                              "--Press 3 to delete an Item\n" +
                              "--Press 4 to view the advanced features\n" +
                              "--Press Enter to exit");
            string input = Console.ReadLine();
            int option;
            if(input == "")
            {
                return 10;
            }
            if(Int32.TryParse(input, out option) && (option <= 4))
            {                
                return option;
            }
            else
            {
                Console.WriteLine("\n*****Please enter a number from the list.*****\n\n");
                return getUserChoice();
            }
        }
        private void helpMenu()
        {
            Console.WriteLine("Thank you for accessing our help menu\n" +
                              "Unfortunately at the early stage of development\n" +
                              "The help menu is not yet available. Please check\n" +
                              "here in future beta releases\n\n\n\n");
        }
        private void addItem()
        {
            string itemName;
            string itemRoom;
            Console.WriteLine("\nPlease enter the name of the item you would like to add: \n" +
                              "or just hit Enter to go back to main menu\n");
            itemName = Console.ReadLine();
            if (itemName == "")
            {
                return;
            }
            Console.WriteLine("\nPlease enter the name of the room the item is in");
            itemRoom = Console.ReadLine();
            Item newItem = new Item(itemName, itemRoom);
            Console.WriteLine($"\nI've added {itemName} to the inventory of {itemRoom}\n");
            Items.Add(newItem);
            int index = rooms.FindIndex(room => room.roomName == itemRoom);
            rooms[index].addItem(newItem);
        }

        private void addRoom()
        {
            string roomName;
            Console.WriteLine("\nPlease enter the name of the room you would like to add \n" +
                              "or just hit Enter to go back to main menu\n");
            roomName = Console.ReadLine();
            if (roomName == "")
            {
                return;
            }
            Room newRoom = new Room(roomName);
            rooms.Add(newRoom);
            Console.WriteLine($"\nI've added {roomName} to your home\n");
        }

        private void deleteItem()
        {
            string delItem;
            Console.WriteLine("\nPlease enter the name of the item you would like to delete: \n" +
                              "or just hit Enter to go back to main menu\n");
            delItem = Console.ReadLine();
            Console.WriteLine("\n\n\n Are you sure? Items that are deleted are not recoverable \n" +
                             $"Make sure you really want to delete {delItem}");
            if(delItem == "")
            {
                return;
            }
            int index = Items.FindIndex(item => item.itemName == delItem);
            var item = Items[index];
            Items.RemoveAt(index);
            int roomIndex = rooms.FindIndex(room => room.roomName == item.roomName);
            rooms[roomIndex].removeItem(item.itemName);
            item = null;
        }
        private void advancedOptions()
        {
            Console.WriteLine("This is the future home of advanced options");
        }
    }
}