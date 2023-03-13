using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS361Project1
{
    internal class HomeData
    {
        private List<Home> homes = new List<Home>();

        public HomeData(List<Home> homes)
        {
            this.homes = homes;
        }
        public void addHome(Home home)
        {
            homes.Add(home);
        }

        public List<Home> GetHomes()
        {
            return homes;
        }

        public void removeHome(string homeName)
        {
            int index = homes.FindIndex(home => home.homeName == homeName);
            if (index != -1)
            {
                homes.RemoveAt(index);
            }
            else
            {
                Console.WriteLine($"Error: Home {homeName} not found.");
            }
        }
    }
}
