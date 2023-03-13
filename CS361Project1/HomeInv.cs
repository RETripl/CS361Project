using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics.Tracing;
using CS361Project1;
using Newtonsoft.Json.Linq;

/*TODO
 * Upgrade everything to use new layer structure. 
 * Add current home functionality.
 * Add rename home functionality. 
 * 
 * 
 * 
 * 
 * 
 * 
 */

namespace HomeInv
{
    public class HomeInv
    {

        //Lists
        
        public List<Item> Items = new List<Item>();
        private List<Room> rooms = new List<Room>();
        private List<Home> homes = new List<Home>();
        
        //Members
        private string currentHouse { get; set; }
        //Options
        private const int HelpOption = 0;
        private const int AddItemOption = 1;
        private const int AddRoomOption = 2;
        private const int DeleteItemOption = 3;
        private const int AdvancedOption = 4;
        private const int SearchItemOption = 5;
        private const int ListAllItemsOption = 6;
        private const int DeleteAllItemsInRoomOption = 7;
        private const int DeleteAllItemsInHouseOption = 8;
        private const int SwitchHomeOption = 9;
        private const int DeleteHomeOption = 10;
        private const int ExitOption = 11;
        public HomeInv()
        {
            HomeData homeData = new HomeData(homes);
            readFile(homeData);
            Console.WriteLine("Thanks for downloading the newest version!\n" +
                              "You can now follow the menu prompts to add\n" +
                              "or remove and item and add new rooms to your \n" +
                              "home\n" +
                              "\n" +
                              "Press Enter");
            Console.ReadLine();
            CheckHome(homeData);
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
                    case 5:
                        searchForItem();
                        break;
                    case 6:
                        listAllItems();
                        break;
                    case 11:
                        running = false;
                        break;
                }
            }

        }


        private int getUserChoice()
        {
            while (true)
            {
                Console.WriteLine("Please enter your choice:");
                Console.WriteLine("0: Help");
                Console.WriteLine("1: Add Item");
                Console.WriteLine("2: Add Room");
                Console.WriteLine("3: Delete Item");
                Console.WriteLine("4: Advanced Options");
                Console.WriteLine("5: Search for Item");
                Console.WriteLine("6: List All Items");
                Console.WriteLine("7: Delete All Items in a Room");
                Console.WriteLine("8: Delete All items in current House");
                Console.WriteLine("9: Switch Homes");
                Console.WriteLine("10: Delete Current Home");
                Console.WriteLine("Enter: Exit");

                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    return ExitOption;
                }

                if (int.TryParse(input, out int option) && option >= HelpOption && option <= DeleteHomeOption)
                {
                    return option;
                }

                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        private void helpMenu()
        {
            Console.WriteLine("Thank you for accessing our help menu\n" +
                              "Unfortunately at the early stage of development\n" +
                              "The help menu is not yet available. Please check\n" +
                              "here in future beta releases\n\n\n\n");
        }

        private void listAllItems()
        {
            if (Items?.Count == 0)
            {
                Console.WriteLine("No Items on your list");
            }
            foreach (Item item in Items)
            {
                Console.WriteLine($"{item.itemName} {item.roomName}\n");
            }
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
            if (index == -1)
            {
                addRoom(itemRoom);
                index = rooms.FindIndex(room => room.roomName == itemRoom);
            }
            rooms[index].addItem(newItem);
        }

        private void addRoom(string roomName = "")
        {
            if (roomName == "")
            {
                Console.WriteLine("\nPlease enter the name of the room you would like to add \n" +
                  "or just hit Enter to go back to main menu\n");
                roomName = Console.ReadLine();
            }

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
            if (delItem == "")
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

        private void searchForItem()
        {
            Console.WriteLine("\nPlease enter the name of the item you would like to search for \n" +
                  "or just hit Enter to go back to main menu\n");
            string searchItem = Console.ReadLine();
            int Index = Items.FindIndex(item => item.itemName == searchItem);
            if (Index == -1)
            {
                searchItem = advancedSearch(searchItem);
                if (searchItem != "Error")
                {
                    Index = Items.FindIndex(item => item.itemName == searchItem);
                    Console.WriteLine($"I found that item! It's in this room {Items[Index].roomName}");
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine($"I found that item! It's in this room {Items[Index].roomName}");
            }
        }

        private string advancedSearch(string advSearch)
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 3000);
            NetworkStream stream = client.GetStream();
            List<string> wordList = new List<string> { };
            foreach (Item item in Items)
            {
                wordList.Add(item.itemName);
            }

            SearchData data = new SearchData { SearchTerm = advSearch, SearchList = wordList };
            string json = JsonConvert.SerializeObject(data);
            byte[] dataBytes = Encoding.UTF8.GetBytes(json);
            stream.Write(dataBytes, 0, dataBytes.Length);
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            List<string> stringList = JsonConvert.DeserializeObject<List<string>>(jsonString);
            Console.WriteLine("Sorry, I couldn't find that item, I have some similar items though.\n\n");
            string userResponse;
            foreach (string word in stringList)
            {
                Console.WriteLine($"Did you mean this item? {word}\n\n");
                userResponse = Console.ReadLine();
                if (userResponse == "yes")
                {
                    return word;
                }
            }
            Console.WriteLine("Sorry, I couldn't find the item you're looking for.");
            return "Error";

        }
        private void advancedOptions()
        {
            Console.WriteLine("This is the future home of advanced options");
        }
        private void readFile(HomeData homeData)
        {
            string json = File.ReadAllText("C:\\OregonState\\cs361\\CS361Project1\\homeInv.txt");
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            JArray data = JArray.Parse(json);
            foreach(var home in data)
            {
                var homeName = (string)home["homeName"];
                Home newHome = new Home(homeName);
                foreach(var room in home["rooms"])
                {
                    var roomName = (string)room["name"];
                    Room newRoom = new Room(roomName);
                    newHome.addRoom(newRoom);
                    foreach(var item in room["items"])
                    {
                        var itemName = (string)item["name"];
                        Item newItem = new Item(itemName, roomName, homeName);
                        newRoom.addItem(newItem);
                        Items.Add(newItem);
                    }
                }
                homeData.addHome(newHome);
            }
            return;
        }

        private void CheckHome(HomeData homeData)
        {
            if (homeData.GetHomes().Count == 0)
            {
                Console.WriteLine("Please enter a new home");
                string newHome = Console.ReadLine();
                addHome(homeData, newHome);
                currentHouse = newHome;
            }
            else
            {
                Console.WriteLine("Please select current home\n");
                int i = 1;
                foreach(Home home in homeData.GetHomes())
                {
                    Console.WriteLine($"Please press {i} for {home}");
                }
                List<Home> houses = new List<Home>(homeData.GetHomes());
                currentHouse = houses[i-1].homeName;
            }
        }
        private void addHome(HomeData homeData, string newHome)
        {
            Home home = new Home(newHome);
            homeData.addHome(home);
            return;
        }

    }




        public class SearchData
    {
        public string SearchTerm { get; set; }
        public List<string> SearchList { get; set; }
    }


}